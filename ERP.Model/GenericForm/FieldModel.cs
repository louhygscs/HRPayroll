﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class FieldModel
    {
        public Guid FieldId { get; set; }
        public Guid? FormId { get; set; }
        public string FieldLabel { get; set; }
        public string FieldType { get; set; }
        public bool? FieldIsRequired { get; set; }
        public string ItemsJson { get; set; }        
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
