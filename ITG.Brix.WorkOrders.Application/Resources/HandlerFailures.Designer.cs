﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITG.Brix.WorkOrders.Application.Resources
{
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
    public class HandlerFailures
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal HandlerFailures()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ITG.Brix.WorkOrders.Application.Resources.HandlerFailures", typeof(HandlerFailures).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Record with specified name already exists..
        /// </summary>
        public static string Conflict
        {
            get
            {
                return ResourceManager.GetString("Conflict", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $filter expression is invalid or unsupported..
        /// </summary>
        public static string InvalidQueryFilter
        {
            get
            {
                return ResourceManager.GetString("InvalidQueryFilter", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to {0} not found..
        /// </summary>
        public static string NotFound
        {
            get
            {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Record has been modified by someone else, review changes before trying again..
        /// </summary>
        public static string NotMet
        {
            get
            {
                return ResourceManager.GetString("NotMet", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Biztalk connectivity issues..
        /// </summary>
        public static string UpstreamAccessBiztalk
        {
            get
            {
                return ResourceManager.GetString("UpstreamAccessBiztalk", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Plato connectivity issues..
        /// </summary>
        public static string UpstreamAccessPlato
        {
            get
            {
                return ResourceManager.GetString("UpstreamAccessPlato", resourceCulture);
            }
        }
    }
}