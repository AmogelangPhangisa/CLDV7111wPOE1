## Create a local.settings.json file in the root directory.

Example:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ConnectionStrings:DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=KhumaloCraft;Trusted_Connection=True;TrustServerCertificate=True",
    "ConnectionStrings:BusinessAPI": "http://localhost:5068",
    "VapidKeys:PublicKey": "<vapidpublickey>",
    "VapidKeys:PrivateKey": "<vapidprivatekey>"
  }
}
```