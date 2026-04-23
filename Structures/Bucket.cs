using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.ExternalLibraries.GoogleCloudStorage_Connector.Structures;

[OSStructure(Description = "Represents a Google Cloud Storage container.")]
public struct Bucket
{
    public Bucket()
    {
        Name = string.Empty;
        Location = string.Empty;
        StorageClass = string.Empty;
        Created = new DateTime(1900, 1, 1);
    }

    [OSStructureField(Description = "The unique name of the bucket.", IsMandatory = true)]
    public string Name { get; set; }

    [OSStructureField(Description = "The geographic region where the bucket's data is stored.")]
    public string Location { get; set; }

    [OSStructureField(Description = "The storage tier of the bucket (e.g., STANDARD).")]
    public string StorageClass { get; set; }

    [OSStructureField(Description = "The date and time (UTC) when the bucket was created.")]
    public DateTime Created { get; set; }
}
