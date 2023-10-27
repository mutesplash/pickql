namespace pickql;

public enum CatalogType : ushort {
    Bestseller = 0, // Formerly PAB
    Standard = 1,   // Subset of what was formerly BAP
    BAM = 2         // Broken and Missing.  Formerly BAP, inacessable for regular ordering.
}

public record LegoElement {
    public int Id {get;} = 0;    // get; set;
    public int DesignNumber {get;} = 0;
    public int ColorNumber {get;} = 0;
    public CatalogType PurchaseType {get;} = CatalogType.BAM;
    public bool Available {get;} = false;
    public string Json {get;} = "{}";
}