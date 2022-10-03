using CommandLine;

namespace img_processing_test;

public class CommandLineOptions
{
    [Value(index: 0, Required = true, HelpText = "Image file Path to process.")]
    public string SourcePath { get; set; }
    [Value(index: 1, Required = true, HelpText = "Path to save.")]
    public string SavePath { get; set; }
}