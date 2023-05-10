using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ZorroCodeAnalyzers
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class ZA0001SlicesIntersector : DiagnosticAnalyzer
  {
    public const string DiagnosticId = "ZA0001";
    private const string Category = "Architecture";
    private const string KeyWordDefault = "Features";
    private string KeyWord = KeyWordDefault;

    private static readonly LocalizableString title = new LocalizableResourceString(nameof(Resources.ZA0001Title), Resources.ResourceManager, typeof(Resources));
    private static readonly LocalizableString messageFormat = new LocalizableResourceString(nameof(Resources.ZA0001MessageFormat), Resources.ResourceManager, typeof(Resources));
    private static readonly LocalizableString description = new LocalizableResourceString(nameof(Resources.ZA0001Description), Resources.ResourceManager, typeof(Resources));

    private static readonly DiagnosticDescriptor rule = new DiagnosticDescriptor(DiagnosticId, title, messageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(rule);

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();

      context.RegisterCompilationStartAction(InitializeSettings);
      context.RegisterSyntaxTreeAction(AnalyzeContext);
    }

    private void InitializeSettings(CompilationStartAnalysisContext context)
    {
      var sourceText = context.Options.AdditionalFiles
        .SingleOrDefault(x => x.Path == "ZorroCodeAnalyzers.json")
        ?.GetText();

      if (sourceText == null)
      {
        return;
      }

      var serializer = new DataContractJsonSerializer(typeof(AnalyzerSettings));

      var settingsText = sourceText.ToString();

      var settings = (AnalyzerSettings)serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(settingsText)));

      KeyWord = settings.ZA0001 ?? KeyWordDefault;
    }

    private void AnalyzeContext(SyntaxTreeAnalysisContext context)
    {
      var root = context.Tree.GetCompilationUnitRoot();

      var namespaceName = root.Members
        .Where(x => x is NamespaceDeclarationSyntax)
        .Select(x => ((NamespaceDeclarationSyntax)x).Name.ToString())
        .FirstOrDefault();

      var namespaceFeatureName = GetFeatureName(namespaceName, KeyWord);

      if (string.IsNullOrEmpty(namespaceFeatureName))
      {
        return;
      }

      var usingNodes = root.Usings
        .Select(x => GetFeatureName(x.Name.ToString(), KeyWord))
        .ToArray();

      var count = 0;
      foreach (var usingItem in usingNodes)
      {
        if (usingItem != null && usingItem != namespaceFeatureName)
        {
          var location = root.Usings[count].Name.GetLocation();
          var diagnostic = Diagnostic.Create(rule, location, namespaceFeatureName, usingItem);
          context.ReportDiagnostic(diagnostic);
        }

        count++;
      }
    }

    private string GetFeatureName(string route, string keyword)
    {
      if (string.IsNullOrEmpty(route))
      {
        return null;
      }

      var segments = route.Split('.');
      var featureKeyPosition = Array.IndexOf(segments, keyword);

      if (featureKeyPosition == -1)
      {
        return null;
      }

      if (featureKeyPosition == segments.Length - 1)
      {
        return null;
      }

      return segments[featureKeyPosition + 1];
    }
  }
}
