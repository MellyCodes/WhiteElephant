/**
* @project White Elephant E-Commerce Website
* @authors Courtney Diotte
* @authors Melanie Roy-Plommer
* @version 1.0
*
* @section DESCRIPTION
* <  >
*
* @section LICENSE
* Copyright 2018 - 2019
* Permission to use, copy, modify, and/or distribute this software for
* any purpose with or without fee is hereby granted, provided that the
* above copyright notice and this permission notice appear in all copies.
*
* THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
* WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
* MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
* ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
* WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
* ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
* OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
*
* @section Academic Integrity
* I certify that this work is solely my own and complies with
* NBCC Academic Integrity Policy (policy 1111)
*/

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