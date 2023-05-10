# ZorroCodeAnalyzers

Roslyn code analyzers

![workflow state](https://github.com/omsdotnet/ZorroCodeAnalyzers/actions/workflows/dotnet.yml/badge.svg?event=push)


# Project Promo:

![1](https://github.com/omsdotnet/ZorroCodeAnalyzers/blob/main/promo/640x320.png?raw=true)


### Purpose of code analyzers

[Roslyn Code Analyzers](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/) - Roslyn compiler extension technology for C#/VB.NET languages, which provides the ability to describe your own rules for errors/warnings and your own code refactorings. Analyzers are supplied as nuget packages, when connected to a project, the compiler automatically picks up analyzers and refactorings and uses them when building. In addition, the Visual Studio development environment also takes into account the connected analyzers, showing messages from them as you work with the program text, and offers refactorings using pop-up bubbles, along with its own built-in refactorings.
Code analyzers are useful for automating code reviews in terms of checking compliance with development standards, as well as for writing custom refactorings.


### List of code analyzers

* [*ZA0001* - Slice namespaces must not overlap](https://github.com/omsdotnet/ZorroCodeAnalyzers/wiki/ZA0001)
* [*ZA0002* - Domain namespaces must not overlap with Application and Infrastructure](https://github.com/omsdotnet/ZorroCodeAnalyzers/wiki/ZA0002)
* [*ZA0003* - Application namespaces must not overlap with Infrastructure](https://github.com/omsdotnet/ZorroCodeAnalyzers/wiki/ZA0003)
* [*ZA0004* - Infrastructure port and adapters must not overlap](https://github.com/omsdotnet/ZorroCodeAnalyzers/wiki/ZA0004)


### Using code analyzers

Add a link to the ZorroCodeAnalyzers nuget-package to your project, to add a link to all projects in the solution, it is convenient to use the mechanism
[Directory.Build.props](https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019#directorybuildprops-and-directorybuildtargets).


### Repository contents

* Solution *src\ZorroCodeAnalyzers* - a solution with a code analyzers and refactorings contains tests and VisualStudio plugin (vsix)
* Solution *src\ZorroCodeAnalyzers.Example* - a solution with a project that references the compiled package ```ZorroCodeAnalyzers``` contains various code examples with analyzer-specific errors and is used to check the correctness of code analyzers
* *promo* - content for promoution this project
