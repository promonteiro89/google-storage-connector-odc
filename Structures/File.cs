using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.ExternalLibraries.GoogleCloudStorage_Connector.Structures;

[OSStructure(Description = "Represents a file's content and metadata.")]
public struct File
{
    public File()
    {
        Content = Array.Empty<byte>();
        ContentType = string.Empty;
    }

    [OSStructureField(Description = "The binary content of the file.", IsMandatory = true)]
    public byte[] Content { get; set; }

    [OSStructureField(Description = "The MIME type of the file (e.g., image/png).")]
    public string ContentType { get; set; }
}
