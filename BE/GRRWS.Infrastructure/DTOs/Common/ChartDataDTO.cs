﻿namespace GRRWS.Infrastructure.DTOs.Common
{
    public class ChartDataDTO
    {
        public string Label { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Color { get; set; } = string.Empty;
        public double Percentage { get; set; }
    }
}
