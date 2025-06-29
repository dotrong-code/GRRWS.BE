#!/bin/bash

# Dừng dịch vụ để cập nhật
systemctl stop back-end.service

# Build và publish project
dotnet publish BE/GRRWS.Host/GRRWS.Host.csproj -c Release -o /var/www/back-end/publish

# Copy Firebase key vào thư mục publish sau khi publish xong
mkdir -p /var/www/back-end/publish/Keys
cp /var/www/back-end/BE/GRRWS.Host/Keys/koiveterinaryservicecent-925db-firebase-adminsdk-vus2r-2b14e43049.json /var/www/back-end/publish/Keys/
chown www-data:www-data /var/www/back-end/publish/Keys/koiveterinaryservicecent-925db-firebase-adminsdk-vus2r-2b14e43049.json
chmod 600 /var/www/back-end/publish/Keys/koiveterinaryservicecent-925db-firebase-adminsdk-vus2r-2b14e43049.json

# Khởi động lại dịch vụ
systemctl start back-end.service

echo "Deploy completed successfully."
