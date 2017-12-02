﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class CategoryDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int? Pid { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
