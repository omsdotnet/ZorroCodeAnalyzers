﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace ZorroCodeAnalyzers
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class ZA0004InfrastructureIntersector : DiagnosticAnalyzer
  {
    public const string DiagnosticId = "ZA0004";
    private const string Category = "Architecture";
    private const string KeyWord = "Infrastructure";

    private static readonly LocalizableString title = new LocalizableResourceString(nameof(Resources.ZA0004Title), Resources.ResourceManager, typeof(Resources));
    private static readonly LocalizableString messageFormat = new LocalizableResourceString(nameof(Resources.ZA0004MessageFormat), Resources.ResourceManager, typeof(Resources));
    private static readonly LocalizableString description = new LocalizableResourceString(nameof(Resources.ZA0004Description), Resources.ResourceManager, typeof(Resources));

    private static readonly DiagnosticDescriptor rule = new DiagnosticDescriptor(DiagnosticId, title, messageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(rule);

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();

      context.RegisterSyntaxTreeAction(AnalyzeContext);
    }

    private static void AnalyzeContext(SyntaxTreeAnalysisContext context)
    {
      var root = context.Tree.GetCompilationUnitRoot();

      var namespaceName = root.Members
        .Where(x => x is NamespaceDeclarationSyntax)
        .Select(x => ((NamespaceDeclarationSyntax)x).Name.ToString())
        .FirstOrDefault();

      var infrastructureItem = GetInfrastructureItem(namespaceName, KeyWord);

      if (string.IsNullOrEmpty(infrastructureItem))
      {
        return;
      }

      var usingNodes = root.Usings
        .Select(x => GetInfrastructureItem(x.Name.ToString(), KeyWord))
        .ToArray();

      var count = 0;
      foreach (var usingItem in usingNodes)
      {
        if (usingItem != null && usingItem != infrastructureItem)
        {
          var location = root.Usings[count].Name.GetLocation();
          var diagnostic = Diagnostic.Create(rule, location, infrastructureItem, usingItem);
          context.ReportDiagnostic(diagnostic);
        }

        count++;
      }
    }

    private static string GetInfrastructureItem(string route, string keyword)
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
