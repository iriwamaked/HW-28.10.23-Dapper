using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperHW
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string City { get; set; }

    }
}
//CREATE TABLE[dbo].[Person]
//(
//    [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
//    Name nvarchar(15) NOT NULL,
//    Age int NOT NULL,
//    City nvarchar(20) NOT NULL
//)