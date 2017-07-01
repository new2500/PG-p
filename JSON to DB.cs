JSON to DB
(Need testing first)

If ask for a list of JSON, but a List of hash table

#region DFS, if ask for a list of JSON

        public List<JSON> parseJSON(JSON apiData, string[] columns)
        {
            List<JSON> res = new List<JSON>();
            helper(apiData, columns, 0, new StringBuilder("{"), res);
            return res;
        }
        private void helper(JSON apiData, string[] columns, int index, StringBuilder path, List<JSON> res)
        {
            if (apiData.Type().Equals("JSONString"))
            {
                path.Append(columns[index]).Append(":").Append(apiData.value()).Append("}");
                res.Add(new JSON(path.ToString()));
            }
            if (apiData.Type().Equals("JSONMapping"))
            {
                foreach (string key in apiData.keys())
                {
                    string item = columns[index] + ":" + key + ",";
                    path.Append(item);
                    helper(apiData.get(key), columns, index + 1, path,res);
                }
            }
        }
//For next level, we always go through until we meet the JSON String => which is the end, so it's full DFS.
//Since we go through all the branch until the end, it's a O(2^n) algorithm.


#endregion


#region If ask for the List of Hash table, then it goes similar logic. with Backtracking. Please test first

       public List<Dictionary<string, string>> parseJSON(JSON apiData, string[] columns)
        {
            List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            if (apiData == null || columns == null)
                return res;
            dfs(apiData, columns, res, 0, new Dictionary<string, string>());
            return res;
        }
        private void dfs(JSON apiData, string[] column, List<Dictionary<string, string>> res,
            int index, Dictionary<string, string> path)
        {
            string[] Keys = apiData.key();  //Get key array
            //Exit:
            //1. If apiData return jsonString, Even though we haven't walk through the column, but we can't go further,
            //So we need to add into res, then backtracking: Delete the last node(hashtable..), and go to another path
            if (apiData.Type() == "JSONString")
            {
                path.Add(column[index], Keys[i]);
                res.Add(new Dictionary<string, string>(path));
                path.Remove(column[i]);
            }
            //2. If we reach the last of column, add path into res
            if (index == column.Length)
            {
                res.Add(new Dictionary<string, string>(path));
            }
            //Using the API to get the key array within the JSONMapping.
            //For example: when column[i] = country(1st level), the they key is {GB, US}, dfs all the key, math keys[i] and column[index]
            for (int i = 0; i < Keys.Length; i++)
            {
                path[column[index]] = Keys[i];
                dfs(apiData.get(Keys[i]), column, res, index + 1, path);
                path.Remove(column[index]);   //Back to the start point, keep looking for next next branch.
            }
            if (apiData.Type())
            {
                JSON json = apiData.get(column);
                string[] keys = apiData.keys;
            }
        }



#endregion
