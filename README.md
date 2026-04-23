# OutSystems Google Cloud Storage Connector

[![Platform](https://img.shields.io/badge/Platform-OutSystems_ODC-red.svg)](https://www.outsystems.com/odc/)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A high-performance, enterprise-grade connector for **Google Cloud Storage (GCS)**, specifically built for **OutSystems Developer Cloud (ODC)**. This library wraps the official Google Cloud .NET SDK, providing a secure and native experience for managing cloud resources.

## 🚀 Features

- **Object Management:** Upload, Download, List, Delete, and Metadata-based existence checks.
- **Bucket Management:** Programmatic creation, deletion, and listing of containers.
- **Signed URLs:** Full support for V4 Signed URLs (GET/PUT) for secure direct-to-browser access.
- **Encapsulated Data Model:** Structured authentication and file objects for cleaner Service Studio logic.
- **ODC Optimized:** Fully compliant with the ODC External Logic SDK, including embedded icons and camelCase naming.

## 📋 Prerequisites

1.  An active **Google Cloud Project**.
2.  A **Service Account** with necessary roles:
    -   `Storage Object Admin` (for file operations)
    -   `Storage Admin` (for bucket management)
    -   `Service Account Token Creator` (required for **Signed URLs**)
3.  A **Service Account JSON Key** (from which you will extract credentials).

## 🔐 Configuration

Instead of storing JSON files, this connector uses a structured `Authentication` object. Map the following values from your Service Account JSON to your ODC App Settings:

-   **ProjectId:** `project_id`
-   **ClientEmail:** `client_email`
-   **PrivateKey:** `private_key` (including `-----BEGIN PRIVATE KEY-----` and `-----END PRIVATE KEY-----`)

## 🛠️ Actions

| Action | Description |
| :--- | :--- |
| `Object_Upload` | Uploads a `File` (Binary + ContentType) to a bucket. |
| `Object_Download` | Retrieves a `File` structure from a specific path. |
| `Object_List` | Returns a list of `Object` metadata, supports prefix filtering. |
| `Object_Exists` | Performs a lightweight check to verify if a file exists. |
| `Object_Delete` | Permanently removes an object. |
| `Object_GetSignedUrl` | Generates a time-limited secure URL for direct access. |
| `Bucket_List` | Lists all buckets in the project. |
| `Bucket_Create` | Creates a new container in a specified location (e.g., `US`, `EU`). |
| `Bucket_Delete` | Deletes an empty bucket. |

## 🏗️ Technical Setup

### Local Build
1.  Ensure you have the [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.
2.  Build and Publish:
    ```bash
    dotnet publish GoogleCloudStorage.csproj -c Release -f net8.0 --no-self-contained
    ```

### ODC Deployment
The deployment package must be a flat ZIP containing only the essential assemblies.
1.  Navigate to the `publish` folder.
2.  Remove `OutSystems.ExternalLibraries.SDK.dll`.
3.  Zip all remaining files and upload to the ODC Portal under **External Logic**.

## ⚖️ Limitations
- **Memory:** Binary data is handled in-memory (`byte[]`). Avoid direct server-side handling of files exceeding 200MB. Use **Signed URLs** for large file transfers.
- **Timeouts:** Long-running uploads are subject to ODC server action timeouts.
- **Uniqueness:** Bucket names must be globally unique across Google Cloud.

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
