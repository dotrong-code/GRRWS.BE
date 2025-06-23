using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Mechanic;
using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Implement.Service
{
    public class MechanicPerformanceService : IMechanicPerformanceService
    {
        private readonly IUnitOfWork _unit;

        public MechanicPerformanceService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<MechanicPerformanceProfile> GetMechanicPerformanceAsync(Guid mechanicId)
        {
            var mechanic = await _unit.UserRepository.GetByIdAsync(mechanicId);


            // Get performance data from separate table
            var performance = await _unit.MechanicPerformanceRepository.GetByMechanicIdAsync(mechanicId);

            if (performance == null)
            {
                // Create initial performance record if doesn't exist
                performance = new MechanicPerformance
                {
                    MechanicId = mechanicId,
                    LastPerformanceUpdate = DateTime.Now
                };
                await _unit.MechanicPerformanceRepository.CreateOrUpdateAsync(performance);
                await _unit.SaveChangesAsync();
            }

            var profile = new MechanicPerformanceProfile
            {
                MechanicId = mechanicId,
                MechanicName = mechanic.FullName,
                AverageTaskCompletionTime = performance.AverageTaskCompletionTime ?? 0,
                AveragePerformanceScore = performance.AveragePerformanceScore ?? 0,
                TotalTasksCompleted = performance.TotalTasksCompleted,
                TotalTasksOnTime = performance.TotalTasksOnTime,
                TotalTasksLate = performance.TotalTasksLate,
                EfficiencyRating = performance.EfficiencyRating ?? 0,
                OnTimePercentage = CalculateOnTimePercentage(performance),
                TaskTypePerformance = GetTaskTypePerformanceFromRecord(performance),
                RecentPerformanceTrend = await GetRecentPerformanceTrendAsync(mechanicId),
                CurrentShiftInfo = await GetCurrentShiftInfoAsync(mechanicId),
                LastPerformanceUpdate = performance.LastPerformanceUpdate,
                TasksRequiredRework = performance.TasksRequiredRework,
                CustomerSatisfactionScore = performance.CustomerSatisfactionScore ?? 0,
                SafetyIncidents = performance.SafetyIncidents
            };

            return profile;
        }

        public async Task UpdateMechanicPerformanceAsync(Guid mechanicId, Guid completedTaskId)
        {
            var task = await _unit.TaskRepository.GetByIdAsync(completedTaskId);
            if (task == null || !task.EndTime.HasValue)
                return;

            // Calculate task metrics
            var taskMetrics = CalculateTaskMetrics(task);

            // Record in performance history
            await RecordPerformanceHistory(mechanicId, task, taskMetrics);

            // Update aggregate performance
            await UpdateAggregatePerformance(mechanicId, task, taskMetrics);
        }

        public async Task<List<MechanicRanking>> GetMechanicRankingsAsync()
        {
            var allPerformances = await _unit.MechanicPerformanceRepository.GetMechanicsWithMinimumTasksAsync(1);

            var rankings = new List<MechanicRanking>();

            foreach (var performance in allPerformances)
            {
                var mechanic = await _unit.UserRepository.GetByIdAsync(performance.MechanicId);
                if (mechanic != null)
                {
                    rankings.Add(new MechanicRanking
                    {
                        MechanicId = performance.MechanicId,
                        MechanicName = mechanic.FullName,
                        PerformanceScore = performance.AveragePerformanceScore ?? 0,
                        EfficiencyRating = performance.EfficiencyRating ?? 0,
                        TasksCompleted = performance.TotalTasksCompleted,
                        OnTimePercentage = CalculateOnTimePercentage(performance),
                        AverageCompletionTime = performance.AverageTaskCompletionTime ?? 0
                    });
                }
            }

            // Sort and assign ranks
            var sortedRankings = rankings
                .OrderByDescending(r => r.PerformanceScore)
                .ThenByDescending(r => r.OnTimePercentage)
                .ThenBy(r => r.AverageCompletionTime)
                .ToList();

            for (int i = 0; i < sortedRankings.Count; i++)
            {
                sortedRankings[i].Rank = i + 1;
            }

            return sortedRankings;
        }

        public async Task<bool> IsMechanicAvailableAsync(Guid mechanicId, DateTime startTime, DateTime endTime)
        {
            // Check if mechanic has scheduled shifts that overlap with the requested time
            var overlappingShifts = await _unit.MechanicShiftRepository.GetOverlappingShiftsAsync(
                mechanicId, startTime, endTime);

            // Check if any overlapping shift has a task assigned or is marked as unavailable
            var hasConflict = overlappingShifts.Any(shift =>
                shift.TaskId.HasValue || !shift.IsAvailable);

            return !hasConflict;
        }

        public async Task<List<User>> GetAvailableMechanicsInShiftAsync(DateTime targetTime, TimeSpan duration)
        {
            var targetDate = targetTime.Date;
            var targetTimeOfDay = targetTime.TimeOfDay;
            var endTime = targetTime.Add(duration);
            var endTimeOfDay = endTime.TimeOfDay;

            // Find which shift the target time falls into
            var applicableShifts = await _unit.ShiftRepository.GetShiftsByTimeRangeAsync(targetTimeOfDay, endTimeOfDay);

            var availableMechanics = new List<User>();

            foreach (var shift in applicableShifts)
            {
                // Get mechanics assigned to this shift on the target date
                var mechanicShifts = await _unit.MechanicShiftRepository.GetMechanicShiftsByShiftAndDateAsync(
                    shift.Id, targetDate);

                var availableInShift = mechanicShifts
                    .Where(ms => ms.IsAvailable && !ms.TaskId.HasValue)
                    .Where(ms => IsTimeSlotAvailable(ms, targetTime, endTime))
                    .Select(ms => ms.Mechanic)
                    .Where(m => m != null)
                    .ToList();

                availableMechanics.AddRange(availableInShift);
            }

            return availableMechanics.Distinct().ToList();
        }

        public async Task<List<MechanicPerformanceHistory>> GetMechanicPerformanceHistoryAsync(Guid mechanicId, int? limit = null)
        {
            return await _unit.MechanicPerformanceHistoryRepository.GetByMechanicIdAsync(mechanicId, limit);
        }

        public async Task<MechanicPerformanceProfile> GetBestMechanicForTaskTypeAsync(TaskType taskType)
        {
            var bestPerformer = await _unit.MechanicPerformanceRepository.GetBestPerformerByTaskTypeAsync(taskType);

            if (bestPerformer == null)
                throw new InvalidOperationException($"No mechanics found for task type {taskType}");

            return await GetMechanicPerformanceAsync(bestPerformer.MechanicId);
        }

        public async Task<List<MechanicPerformanceProfile>> GetTopPerformersAsync(int count)
        {
            var topPerformers = await _unit.MechanicPerformanceRepository.GetTopPerformersAsync(count);
            var profiles = new List<MechanicPerformanceProfile>();

            foreach (var performer in topPerformers)
            {
                var profile = await GetMechanicPerformanceAsync(performer.MechanicId);
                profiles.Add(profile);
            }

            return profiles;
        }

        public async Task MarkTaskAsRequiredReworkAsync(Guid taskId, string reason)
        {
            var performanceHistory = await _unit.MechanicPerformanceHistoryRepository.GetByTaskIdAsync(taskId);

            if (performanceHistory != null)
            {
                performanceHistory.RequiredRework = true;
                performanceHistory.Notes = $"Rework required: {reason}";
                performanceHistory.ModifiedDate = DateTime.Now;

                await _unit.MechanicPerformanceHistoryRepository.UpdateAsync(performanceHistory);

                // Update aggregate performance
                var performance = await _unit.MechanicPerformanceRepository.GetByMechanicIdAsync(performanceHistory.MechanicId);
                if (performance != null)
                {
                    performance.TasksRequiredRework++;
                    performance.LastPerformanceUpdate = DateTime.Now;
                    await _unit.MechanicPerformanceRepository.UpdateAsync(performance);
                }

                await _unit.SaveChangesAsync();
            }
        }

        // Private helper methods
        private async Task RecordPerformanceHistory(Guid mechanicId, Tasks task, TaskCompletionMetrics metrics)
        {
            var historyRecord = new MechanicPerformanceHistory
            {
                Id = Guid.NewGuid(),
                MechanicId = mechanicId,
                TaskId = task.Id,
                TaskType = (TaskType)task.TaskType,
                TaskStartTime = task.StartTime ?? DateTime.Now,
                TaskEndTime = task.EndTime ?? DateTime.Now,
                TaskExpectedTime = task.ExpectedTime,
                ActualDurationMinutes = metrics.ActualDuration,
                ExpectedDurationMinutes = task.ExpectedTime.HasValue && task.StartTime.HasValue
                    ? (task.ExpectedTime.Value - task.StartTime.Value).TotalMinutes
                    : null,
                IsOnTime = metrics.IsOnTime,
                TimeVarianceMinutes = metrics.TimeVariance,
                QualityScore = metrics.QualityScore,
                RequiredRework = false,
                RecordedDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };

            await _unit.MechanicPerformanceHistoryRepository.CreateAsync(historyRecord);
        }

        private async Task UpdateAggregatePerformance(Guid mechanicId, Tasks task, TaskCompletionMetrics metrics)
        {
            var performance = await _unit.MechanicPerformanceRepository.GetByMechanicIdAsync(mechanicId);

            if (performance == null)
            {
                performance = new MechanicPerformance
                {
                    Id = Guid.NewGuid(),
                    MechanicId = mechanicId,
                    CreatedDate = DateTime.Now
                };
            }

            // Update task counts
            performance.TotalTasksCompleted++;
            if (metrics.IsOnTime)
                performance.TotalTasksOnTime++;
            else
                performance.TotalTasksLate++;

            // Update task type specific counts and averages
            UpdateTaskTypeSpecificMetrics(performance, (TaskType)task.TaskType, metrics);

            // Update overall averages
            UpdateOverallAverages(performance, metrics);

            // Recalculate efficiency rating
            performance.EfficiencyRating = CalculateEfficiencyRating(performance);
            performance.LastPerformanceUpdate = DateTime.Now;
            performance.ModifiedDate = DateTime.Now;

            await _unit.MechanicPerformanceRepository.CreateOrUpdateAsync(performance);
        }

        private void UpdateTaskTypeSpecificMetrics(MechanicPerformance performance, TaskType taskType, TaskCompletionMetrics metrics)
        {
            switch (taskType)
            {
                case TaskType.Repair:
                    performance.RepairTasksCompleted++;
                    performance.AverageRepairTime = UpdateRunningAverage(
                        performance.AverageRepairTime,
                        metrics.ActualDuration,
                        performance.RepairTasksCompleted);
                    break;

                case TaskType.Installation:
                    performance.InstallationTasksCompleted++;
                    performance.AverageInstallationTime = UpdateRunningAverage(
                        performance.AverageInstallationTime,
                        metrics.ActualDuration,
                        performance.InstallationTasksCompleted);
                    break;

                case TaskType.Uninstallation:
                    performance.UninstallationTasksCompleted++;
                    performance.AverageUninstallationTime = UpdateRunningAverage(
                        performance.AverageUninstallationTime,
                        metrics.ActualDuration,
                        performance.UninstallationTasksCompleted);
                    break;

                case TaskType.WarrantySubmission:
                    performance.WarrantyTasksCompleted++;
                    performance.AverageWarrantyProcessingTime = UpdateRunningAverage(
                        performance.AverageWarrantyProcessingTime,
                        metrics.ActualDuration,
                        performance.WarrantyTasksCompleted);
                    break;
            }
        }

        private void UpdateOverallAverages(MechanicPerformance performance, TaskCompletionMetrics metrics)
        {
            // Update overall average completion time
            performance.AverageTaskCompletionTime = UpdateRunningAverage(
                performance.AverageTaskCompletionTime,
                metrics.ActualDuration,
                performance.TotalTasksCompleted);

            // Update overall performance score
            performance.AveragePerformanceScore = UpdateRunningAverage(
                performance.AveragePerformanceScore,
                metrics.QualityScore,
                performance.TotalTasksCompleted);
        }

        private double UpdateRunningAverage(double? currentAverage, double newValue, int totalCount)
        {
            if (!currentAverage.HasValue || totalCount <= 1)
                return newValue;

            var totalPrevious = currentAverage.Value * (totalCount - 1);
            return (totalPrevious + newValue) / totalCount;
        }

        private TaskCompletionMetrics CalculateTaskMetrics(Tasks task)
        {
            var metrics = new TaskCompletionMetrics();

            if (task.StartTime.HasValue && task.EndTime.HasValue)
            {
                metrics.ActualDuration = (task.EndTime.Value - task.StartTime.Value).TotalMinutes;
            }

            if (task.ExpectedTime.HasValue && task.EndTime.HasValue)
            {
                metrics.IsOnTime = task.EndTime.Value <= task.ExpectedTime.Value;
                metrics.TimeVariance = (task.EndTime.Value - task.ExpectedTime.Value).TotalMinutes;
            }

            metrics.QualityScore = CalculateTaskQualityScore(task);
            return metrics;
        }

        private double CalculateTaskQualityScore(Tasks task)
        {
            double score = 100.0;

            if (task.ExpectedTime.HasValue && task.EndTime.HasValue)
            {
                var lateness = (task.EndTime.Value - task.ExpectedTime.Value).TotalMinutes;
                if (lateness > 0)
                {
                    score -= Math.Min(lateness * 0.5, 30);
                }
                else if (lateness < 0 && Math.Abs(lateness) <= 30)
                {
                    score += Math.Min(Math.Abs(lateness) * 0.3, 10);
                }
                else if (lateness < -30)
                {
                    score -= 5;
                }
            }

            return Math.Max(0, Math.Min(100, score));
        }

        private double CalculateOnTimePercentage(MechanicPerformance performance)
        {
            if (performance.TotalTasksCompleted == 0)
                return 0;

            return Math.Round((double)performance.TotalTasksOnTime / performance.TotalTasksCompleted * 100, 2);
        }

        private double CalculateEfficiencyRating(MechanicPerformance performance)
        {
            if (performance.TotalTasksCompleted == 0)
                return 0;

            var onTimeRate = CalculateOnTimePercentage(performance) / 100.0;
            var qualityScore = (performance.AveragePerformanceScore ?? 50) / 100.0;

            return Math.Round((onTimeRate * 0.6 + qualityScore * 0.4) * 100, 2);
        }

        private List<TaskTypePerformance> GetTaskTypePerformanceFromRecord(MechanicPerformance performance)
        {
            var taskTypePerformances = new List<TaskTypePerformance>();

            if (performance.RepairTasksCompleted > 0)
            {
                taskTypePerformances.Add(new TaskTypePerformance
                {
                    TaskType = TaskType.Repair.ToString(),
                    TasksCompleted = performance.RepairTasksCompleted,
                    AverageCompletionTime = performance.AverageRepairTime ?? 0,
                    OnTimePercentage = 0 // Could calculate separately if needed
                });
            }

            if (performance.InstallationTasksCompleted > 0)
            {
                taskTypePerformances.Add(new TaskTypePerformance
                {
                    TaskType = TaskType.Installation.ToString(),
                    TasksCompleted = performance.InstallationTasksCompleted,
                    AverageCompletionTime = performance.AverageInstallationTime ?? 0,
                    OnTimePercentage = 0
                });
            }

            if (performance.UninstallationTasksCompleted > 0)
            {
                taskTypePerformances.Add(new TaskTypePerformance
                {
                    TaskType = TaskType.Uninstallation.ToString(),
                    TasksCompleted = performance.UninstallationTasksCompleted,
                    AverageCompletionTime = performance.AverageUninstallationTime ?? 0,
                    OnTimePercentage = 0
                });
            }

            if (performance.WarrantyTasksCompleted > 0)
            {
                taskTypePerformances.Add(new TaskTypePerformance
                {
                    TaskType = TaskType.WarrantySubmission.ToString(),
                    TasksCompleted = performance.WarrantyTasksCompleted,
                    AverageCompletionTime = performance.AverageWarrantyProcessingTime ?? 0,
                    OnTimePercentage = 0
                });
            }

            return taskTypePerformances;
        }

        private async Task<List<PerformanceTrendPoint>> GetRecentPerformanceTrendAsync(Guid mechanicId)
        {
            var recentHistory = await _unit.MechanicPerformanceHistoryRepository.GetRecentPerformanceAsync(mechanicId, 30);

            return recentHistory
                .GroupBy(h => h.RecordedDate.Date)
                .OrderBy(g => g.Key)
                .Select(g => new PerformanceTrendPoint
                {
                    Date = g.Key,
                    TasksCompleted = g.Count(),
                    AverageScore = g.Average(h => h.QualityScore),
                    OnTimePercentage = g.Count(h => h.IsOnTime) * 100.0 / g.Count()
                })
                .ToList();
        }

        private async Task<CurrentShiftInfo?> GetCurrentShiftInfoAsync(Guid mechanicId)
        {
            var now = DateTime.Now;
            var currentShift = await _unit.MechanicShiftRepository.GetCurrentShiftAsync(mechanicId, now);

            if (currentShift?.Shift == null)
                return null;

            return new CurrentShiftInfo
            {
                ShiftName = currentShift.Shift.ShiftName,
                StartTime = currentShift.StartTime ?? now.Date.Add(currentShift.Shift.StartTime),
                EndTime = currentShift.EndTime ?? now.Date.Add(currentShift.Shift.EndTime),
                IsAvailable = currentShift.IsAvailable,
                CurrentTaskId = currentShift.TaskId
            };
        }

        private bool IsTimeSlotAvailable(MechanicShift mechanicShift, DateTime startTime, DateTime endTime)
        {
            if (mechanicShift.Shift == null) return false;

            var shiftStart = mechanicShift.StartTime ?? startTime.Date.Add(mechanicShift.Shift.StartTime);
            var shiftEnd = mechanicShift.EndTime ?? startTime.Date.Add(mechanicShift.Shift.EndTime);

            return startTime >= shiftStart && endTime <= shiftEnd;
        }
    }

    // Supporting classes
    public class TaskCompletionMetrics
    {
        public double ActualDuration { get; set; }
        public bool IsOnTime { get; set; }
        public double TimeVariance { get; set; }
        public double QualityScore { get; set; }
    }
}

