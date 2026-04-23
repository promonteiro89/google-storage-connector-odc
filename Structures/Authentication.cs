using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.ExternalLibraries.GoogleCloudStorage_Connector.Structures;

[OSStructure(Description = "Google Cloud Service Account credentials.")]
public struct Authentication
{
    public Authentication()
    {
        ProjectId = string.Empty;
        ClientEmail = string.Empty;
        PrivateKey = string.Empty;
    }

    [OSStructureField(Description = "The unique ID of your Google Cloud Project.", IsMandatory = true)]
    public string ProjectId { get; set; }

    [OSStructureField(Description = "The 'client_email' from your Service Account JSON key.", IsMandatory = true)]
    public string ClientEmail { get; set; }

    [OSStructureField(Description = "The 'private_key' from your Service Account JSON key.", IsMandatory = true)]
    public string PrivateKey { get; set; }
}
