using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data.Configuration
{
    public class DepartmentConfig :IEntityTypeConfiguration<Department>
    {
      

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(s => s.Name).IsRequired();
        }
    }
}
