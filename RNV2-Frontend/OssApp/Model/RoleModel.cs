using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssApp.Model
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleModel() { }
        public RoleModel(RoleModel row) 
        {
            Id = row.Id;
            Name = row.Name;
        }
        public void CopyFrom(RoleModel row) 
        {
            Name = row.Name;
        }

    }
}
