﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZorroCodeAnalyzers {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ZorroCodeAnalyzers.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Feature namespaces must not overlap.
        /// </summary>
        internal static string ZA0001Description {
            get {
                return ResourceManager.GetString("ZA0001Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Feature &apos;{0}&apos; overlap &apos;{1}&apos;.
        /// </summary>
        internal static string ZA0001MessageFormat {
            get {
                return ResourceManager.GetString("ZA0001MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Features intersector.
        /// </summary>
        internal static string ZA0001Title {
            get {
                return ResourceManager.GetString("ZA0001Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Domain namespaces must not overlap with Application and Infrastructure.
        /// </summary>
        internal static string ZA0002Description {
            get {
                return ResourceManager.GetString("ZA0002Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Domain overlap &apos;{0}&apos;.
        /// </summary>
        internal static string ZA0002MessageFormat {
            get {
                return ResourceManager.GetString("ZA0002MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Domain intersector.
        /// </summary>
        internal static string ZA0002Title {
            get {
                return ResourceManager.GetString("ZA0002Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application namespaces must not overlap with Infrastructure.
        /// </summary>
        internal static string ZA0003Description {
            get {
                return ResourceManager.GetString("ZA0003Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application overlap &apos;{0}&apos;.
        /// </summary>
        internal static string ZA0003MessageFormat {
            get {
                return ResourceManager.GetString("ZA0003MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application intersector.
        /// </summary>
        internal static string ZA0003Title {
            get {
                return ResourceManager.GetString("ZA0003Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Infrastructure port and adapters must not overlap.
        /// </summary>
        internal static string ZA0004Description {
            get {
                return ResourceManager.GetString("ZA0004Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Infrastructure item &apos;{0}&apos; overlap &apos;{1}&apos;.
        /// </summary>
        internal static string ZA0004MessageFormat {
            get {
                return ResourceManager.GetString("ZA0004MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Infrastructure intersector.
        /// </summary>
        internal static string ZA0004Title {
            get {
                return ResourceManager.GetString("ZA0004Title", resourceCulture);
            }
        }
    }
}