﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriorAuthNowCodeExercise.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}