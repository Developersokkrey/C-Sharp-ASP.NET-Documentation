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
--------------------------------------------Push Json String to Stored Procedure  ------------------------------
    //1.C#
    public void CreateCustomerList(List<Customer> customerList)
    {
        string jsonString = JsonConvert.SerializeObject(customerList, Formatting.None);
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("sp_ParseJSON", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // Pass the JSON string as a parameter
                cmd.Parameters.AddWithValue("@json", jsonString);
                // Execute the stored procedure
                SqlDataReader reader = cmd.ExecuteReader();
                // Do something with the result
            }
        }
    }
                                                *******
    //2.T-SQL
    ALTER PROCEDURE sp_ParseJSON
     @json NVARCHAR(MAX)
        AS
        BEGIN
          INSERT INTO [CUSMER] ([CustomerID],[CompID],[Code],[Name1],[Name2],[Phone],[Address],[Location])
            SELECT NEWID(),[CompID],[Code],[Name1],[Name2],[Phone],[Address],[Location]
            FROM OPENJSON(@Json)
            WITH (
                [CompID] NVARCHAR(MAX),
                [Code] NVARCHAR(MAX),
                [Name1] NVARCHAR(MAX),
                [Name2] NVARCHAR(MAX),        
                [Phone] NVARCHAR(MAX),        
                [Address] NVARCHAR(MAX),        
                [Location] NVARCHAR(MAX)      
            )
        END
    --EXEC sp_ParseJSON '[{"CustomerID":"3fa85f64-5717-4562-b3fc-2c963f66afa6","CompID":"93cfc975-517d-43c8-841b-766ec4d53f79","Code":"CU001","Name1":"CU001","Name2":"CU001","Phone":"CU001","Address":"CU001","Location":"CU001","Company":null},{"CustomerID":"3fa85f64-5717-4562-b3fc-2c963f66afa6","CompID":"93cfc975-517d-43c8-841b-766ec4d53f79","Code":"CU002","Name1":"CU002","Name2":"CU002","Phone":"CU002","Address":"CU002","Location":"CU002","Company":null},{"CustomerID":"3fa85f64-5717-4562-b3fc-2c963f66afa6","CompID":"93cfc975-517d-43c8-841b-766ec4d53f79","Code":"CU003","Name1":"CU003","Name2":"CU003","Phone":"CU003","Address":"CU003","Location":"CU003","Company":null},{"CustomerID":"3fa85f64-5717-4562-b3fc-2c963f66afa6","CompID":"93cfc975-517d-43c8-841b-766ec4d53f79","Code":"CU004","Name1":"CU004","Name2":"CU004","Phone":"CU004","Address":"CU004","Location":"CU004","Company":null},{"CustomerID":"3fa85f64-5717-4562-b3fc-2c963f66afa6","CompID":"93cfc975-517d-43c8-841b-766ec4d53f79","Code":"CU005","Name1":"CU005","Name2":"CU005","Phone":"CU005","Address":"CU005","Location":"CU005","Company":null}]'
  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
