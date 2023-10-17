////////////////////////////// <!-- Get Data from Sql function to DataSet & Convert DataSet to list -->  ////////////////////////////////

public async Task<List<Branch>> GetAllBranch()
{
    DataSet ds = new DataSet();
    List<Branch> branches = new List<Branch>();
    using (SqlConnection con = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM GetAll_Branches()";
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Connection = con;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(ds);
            }
        }
        var branch = ds.Tables[0].AsEnumerable()
        .Select(dataRow => new Branch
        {
            Name = (string)dataRow["Name"],
            Code = (string)dataRow["Code"]
            //Name = dataRow.Field<string>("Name"),
            //Code = dataRow.Field<string>("Code")
        }).ToList();
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    branches.Add(new Branch
        //    {
        //        BranchID = (ulong)Convert.ToInt32(dr["BranchID"]),
        //        Name = Convert.ToString(dr["Name"]),
        //        Code = Convert.ToString(dr["Code"]),
        //        Address = Convert.ToString(dr["Address"]),
        //        Location = Convert.ToString(dr["Location"]),
        //        CompID = (ulong)Convert.ToInt32(dr["CompID"])
        //    });
        //}
        return branch;
    }
--------------------------------------------------------------------------------------------------
public IEnumerable<object> GetAllBranchAsEnumerable()
{
    //return new List<T>();
    DataSet ds = new DataSet();
    List<Branch> branches = new List<Branch>();
    using (SqlConnection con = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM GetAll_Branches()";
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Connection = con;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(ds);
            }
        }
        var branch = ds.Tables[0].AsEnumerable()
        .Select(dataRow => new Branch
        {
            Name = (string)dataRow["Name"],
            Code = (string)dataRow["Code"]
            //Name = dataRow.Field<string>("Name"),
            //Code = dataRow.Field<string>("Code")
        }).ToList();
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    branches.Add(new Branch
        //    {
        //        BranchID = (ulong)Convert.ToInt32(dr["BranchID"]),
        //        Name = Convert.ToString(dr["Name"]),
        //        Code = Convert.ToString(dr["Code"]),
        //        Address = Convert.ToString(dr["Address"]),
        //        Location = Convert.ToString(dr["Location"]),
        //        CompID = (ulong)Convert.ToInt32(dr["CompID"])
        //    });
        //}
        return branch;
    }
----------------------------------------------JsonConvert.SerializeObject---------------------------------------------------
public IEnumerable<Branch> GetAllBranchAsEnumerable()
{
    DataSet ds = new DataSet();
    using (SqlConnection con = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM GetAll_Branches()";
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Connection = con;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(ds);
            }
        }
        string json = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
        List<Branch> branch = JsonConvert.DeserializeObject<List<Branch>>(json);
        return branch;
    }
}
        throw;
    }
}
  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
