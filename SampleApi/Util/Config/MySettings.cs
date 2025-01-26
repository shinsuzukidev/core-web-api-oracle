namespace SampleApi.Util.Config
{
    //{
    //  "title": "JSON Schema for my JSON file format",
    //  "type": "object",

    //  "properties": {
    //    "name": {
    //      "type": "string",
    //      "description": "This shows up in tooltips for the 'name' property."
    //    }
    //  }
    //}


    public class MySettings
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public Properties? Properties { get; set; }
    }
    
    public class Properties
    {
        public Name? Name { get; set; }
    }

    public class Name
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
