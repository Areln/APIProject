using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProject.Models
{
	public class SearchListModel
	{
		public SearchResultsRoot Results { get; set; }
		public List<string> TitleList { get; set; }

		public SearchListModel(SearchResultsRoot results, List<string> titleList)
		{
			Results = results;
			TitleList = titleList;
		}
	}
}
