﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Domain.Dtos
{
    public class AccountInfoDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        public RoleType RoleType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Create { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Update { get; set; }

        [Required]
        public bool Enabled { get; set; }

        public virtual List<AccountPropertyInfoDto> Properties { get; set; }

    }





}
