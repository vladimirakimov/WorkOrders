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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CustomFailures
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomFailures()
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ITG.Brix.WorkOrders.Application.Resources.CustomFailures", typeof(CustomFailures).Assembly);
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
        ///   Looks up a localized string similar to PlatoOrder was not created..
        /// </summary>
        public static string CreatePlatoOrderFailure
        {
            get
            {
                return ResourceManager.GetString("CreatePlatoOrderFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to WorkOrder was not created..
        /// </summary>
        public static string CreateWorkOrderFailure
        {
            get
            {
                return ResourceManager.GetString("CreateWorkOrderFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to WorkOrder was not deleted..
        /// </summary>
        public static string DeleteWorkOrderFailure
        {
            get
            {
                return ResourceManager.GetString("DeleteWorkOrderFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to WorkOrder cannot be read..
        /// </summary>
        public static string GetWorkOrderFailure
        {
            get
            {
                return ResourceManager.GetString("GetWorkOrderFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to WorkOrders cannot be read..
        /// </summary>
        public static string ListWorkOrderFailure
        {
            get
            {
                return ResourceManager.GetString("ListWorkOrderFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $skip must be a sequence of digits..
        /// </summary>
        public static string SkipInvalid
        {
            get
            {
                return ResourceManager.GetString("SkipInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $skip must be greater or equal to 0 and less than {0}..
        /// </summary>
        public static string SkipRange
        {
            get
            {
                return ResourceManager.GetString("SkipRange", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $top must be a sequence of digits..
        /// </summary>
        public static string TopInvalid
        {
            get
            {
                return ResourceManager.GetString("TopInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $top must be greater than 0 and less than {0}..
        /// </summary>
        public static string TopRange
        {
            get
            {
                return ResourceManager.GetString("TopRange", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to WorkOrder was not updated..
        /// </summary>
        public static string UpdateWorkOrderFailure
        {
            get
            {
                return ResourceManager.GetString("UpdateWorkOrderFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to WorkOrder not found..
        /// </summary>
        public static string WorkOrderNotFound
        {
            get
            {
                return ResourceManager.GetString("WorkOrderNotFound", resourceCulture);
            }
        }
    }
}
