# Redis Backup
An application that automatically creates backups of Redis 'dump.rdb' files.

## How to use
* Set a path for the folder containing your redis server.
  * When pasting the path, you will need to replace all of the '\\' chars with '/'
* Configure the time limit and destination folder to your preferences.
* Run RedisBackup.exe.

## settings.json
```json
{
  "TimeLimit": 15,
  "DestFolder": "backups",
  "RedisFolder": ""
}
```
* TimeLimit: Time interval for a backup to be made.
* DestFolder: Name of the folder containing the backups made.
* RedisFolder: Folder containing the redis server.
