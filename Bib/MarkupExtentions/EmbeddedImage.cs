using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bib.MarkupExtentions
{
    [ContentProperty("ResourceId")]
    class EmbeddedImage : IMarkupExtension
    {   
        public string ResourceId { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ResourceId.Equals(null))
                return null;

            return ImageSource.FromResource(ResourceId);
        }
    }
}
