using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SPMS.Helpers
{
	public static class HtmlExtensions
	{
		public static IEnumerable<SelectListItem> GetEnumValueSelectList<TEnum>(this IHtmlHelper htmlHelper) where TEnum : struct
		{
			return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
				.Select(x =>
					new SelectListItem
					{
						Text = x.GetType().GetField(x.ToString()).GetCustomAttribute<DisplayAttribute>()?.Name,
						Value = x.ToString()
					}), "Value", "Text");
		}
	}
}
