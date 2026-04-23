# Google Cloud Storage (GCS) Connector for ODC

An enterprise-grade, high-performance External Logic component for **OutSystems Developer Cloud (ODC)**. This connector provides a stateless wrapper around the official [Google Cloud Storage .NET SDK](https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Storage.V1/latest), enabling seamless integration with GCS while adhering to modern cloud architectural best practices.

---

## 🏛 Architecture & Design

The connector is designed as a **stateless adapter**, prioritizing memory efficiency and security. By decoupling the OutSystems interface from the underlying SDK, it ensures long-term maintainability and performance.

### Stateless Adapter Pattern
The `StorageClient` is initialized per request, ensuring no cross-request state contamination and optimal memory management. This is critical for ODC high-concurrency environments.

### Security Strategy: Zero-Persistence
Credentials are never stored or cached within the extension. They are passed as encrypted App Settings at runtime via a structured `Authentication` model.

*   **V4 Signed URLs:** The connector implements cryptographically secure URL signing. This allows direct browser-to-cloud data transfer, bypassing the ODC server to save bandwidth and reduce RAM pressure.

---

## ✨ Component Capabilities

### Object Management
*   **Object_Upload**: Persists objects using an encapsulated `File` structure.
*   **Object_Download**: Retrieves content and system metadata as a single record.
*   **Object_List**: collection-based listing with hierarchical prefix filtering.
*   **Object_Exists**: Lightweight metadata probe to verify paths without data transfer.
*   **Object_Delete**: Permanent removal of cloud assets.
*   **Object_GetSignedUrl**: Generates temporary GET links for direct asset delivery.

### Bucket Management
*   **Bucket_List**: Project-wide container auditing.
*   **Bucket_Create**: Regionalized provisioning of globally unique buckets.
*   **Bucket_Delete**: Decommissioning of empty storage containers.

---

## 📋 Getting Started

### Prerequisites
- [OutSystems Developer Cloud (ODC)](https://www.outsystems.com/odc/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Google Cloud Project with Billing enabled.

### Build and Deploy
1.  **Publish the project:**
    ```bash
    dotnet publish GoogleCloudStorage.csproj -c Release -f net8.0 --no-self-contained
    ```
2.  **Clean the package:** Navigate to the `publish` directory and delete `OutSystems.ExternalLibraries.SDK.dll`.
3.  **Deploy:** Zip the remaining files and upload the archive to the ODC Portal under **External Logic**.

---

## 🔐 Configuration

Map the following values from your **Service Account JSON** to your ODC App Settings:

| Setting | JSON Key | Purpose |
| :--- | :--- | :--- |
| **ProjectId** | `project_id` | Resource lookup and billing scope. |
| **ClientEmail** | `client_email` | Service Account identity. |
| **PrivateKey** | `private_key` | RSA Key for request signing. |

---

## 📄 License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.

---
*Maintained by Paulo Ricardo Oliveira Monteiro.*
