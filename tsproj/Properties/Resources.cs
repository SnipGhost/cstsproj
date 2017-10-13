namespace tsproj.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
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

        internal static Bitmap icons_player1_pause =>
            ((Bitmap) ResourceManager.GetObject("icons_player1_pause", resourceCulture));

        internal static Bitmap icons_player1_play =>
            ((Bitmap) ResourceManager.GetObject("icons_player1_play", resourceCulture));

        internal static Bitmap icons_player1_stop =>
            ((Bitmap) ResourceManager.GetObject("icons_player1_stop", resourceCulture));

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("tsproj.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap test_noimage1 =>
            ((Bitmap) ResourceManager.GetObject("test_noimage1", resourceCulture));
    }
}

