using PdfSharp.Fonts;
using System.IO;
using System.Reflection;

namespace DataLayer.Utilities;
public class VerdanaFontResolver : IFontResolver
{
	public static readonly VerdanaFontResolver Instance = new VerdanaFontResolver();

	public string DefaultFontName => "Verdana";

	public byte[] GetFont(string faceName)
	{
		// Adjust resource names as needed
		var assembly = Assembly.GetExecutingAssembly();
		string resource = faceName switch
		{
			"Verdana#Regular" => "OOPPROJ.Resources.Fonts.Verdana.ttf",
			"Verdana#Bold" => "OOPPROJ.Resources.Fonts.Verdana-Bold.ttf",
			_ => "OOPPROJ.Resources.Fonts.Verdana.ttf"
		};

		using var stream = assembly.GetManifestResourceStream(resource);
		if (stream == null)
			throw new FileNotFoundException($"Font resource '{resource}' not found.");
		using var ms = new MemoryStream();
		stream.CopyTo(ms);
		return ms.ToArray();
	}

	public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
	{
		if (familyName.Equals("Verdana", StringComparison.OrdinalIgnoreCase))
		{
			if (isBold)
				return new FontResolverInfo("Verdana#Bold");
			return new FontResolverInfo("Verdana#Regular");
		}
		// Fallback to Verdana
		return new FontResolverInfo("Verdana#Regular");
	}
}