using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhiteElephantWebsite
{
    public partial class ucProductSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        string[] removedSearchWords = { "the", "at", "a", "and", "or", "this", "is" };

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //build my full query string for search
            string queryString = $"{QueryStringBuilder()}&all={this.chkAllWords.Checked}";

            Response.Redirect($"~/Products.aspx{queryString}");
        }

        private string QueryStringBuilder()
        {
            string queryStringBuild = "?";
            string[] filteredWords = FilteredSearchWords();

            //we only support 5 words 
            int counter = filteredWords.Count() > 5 ? 4 : filteredWords.Count() - 1;

            for (int i = 0; i <= counter; i++)
            {
                queryStringBuild += i == 0 ? $"word{i + 1}={filteredWords[i]}" : $"&word{i + 1}={filteredWords[i]}";
            }

            return queryStringBuild;
        }

        private string[] FilteredSearchWords()
        {
            string[] words = this.txtSearch.Text.Split(' ');
            List<string> cleanedWords = new List<string>();

            foreach (string word in words)
            {
                if (removedSearchWords.Where(w => w.ToLower() == word.ToLower()).Count() == 0)
                {
                    cleanedWords.Add(word);
                }
            }
            return cleanedWords.ToArray();

        }
    }
}