﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserManager.Resources {
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
    public class General {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal General() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UserManager.Resources.General", typeof(General).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attention!.
        /// </summary>
        public static string AttentionTitle {
            get {
                return ResourceManager.GetString("AttentionTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot open same windows simultaneously.
        /// </summary>
        public static string CannotOpenWindowsTwice {
            get {
                return ResourceManager.GetString("CannotOpenWindowsTwice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete.
        /// </summary>
        public static string Delete {
            get {
                return ResourceManager.GetString("Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to delete user &apos;{0}&apos;?.
        /// </summary>
        public static string DeleteUserQuestion {
            get {
                return ResourceManager.GetString("DeleteUserQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete user.
        /// </summary>
        public static string DeleteUserTitle {
            get {
                return ResourceManager.GetString("DeleteUserTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error loading roles.
        /// </summary>
        public static string ErrorLoadingRolesTitle {
            get {
                return ResourceManager.GetString("ErrorLoadingRolesTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error loading users.
        /// </summary>
        public static string ErrorLoadingUsersTitle {
            get {
                return ResourceManager.GetString("ErrorLoadingUsersTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Change was saved. Restart the application to have the new language.
        /// </summary>
        public static string LanguageChangeMessage {
            get {
                return ResourceManager.GetString("LanguageChangeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Language change saved.
        /// </summary>
        public static string LanguageChangeTitle {
            get {
                return ResourceManager.GetString("LanguageChangeTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Roles.
        /// </summary>
        public static string Roles {
            get {
                return ResourceManager.GetString("Roles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name = {0}, Id = {1}.
        /// </summary>
        public static string UserCreatedMessage {
            get {
                return ResourceManager.GetString("UserCreatedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User created.
        /// </summary>
        public static string UserCreatedTitle {
            get {
                return ResourceManager.GetString("UserCreatedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error in creation.
        /// </summary>
        public static string UserCreationFailedTitle {
            get {
                return ResourceManager.GetString("UserCreationFailedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User &apos;{0}&apos; was deleted.
        /// </summary>
        public static string UserDeletedMessage {
            get {
                return ResourceManager.GetString("UserDeletedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User deleted.
        /// </summary>
        public static string UserDeletedTitle {
            get {
                return ResourceManager.GetString("UserDeletedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Users.
        /// </summary>
        public static string Users {
            get {
                return ResourceManager.GetString("Users", resourceCulture);
            }
        }
    }
}
