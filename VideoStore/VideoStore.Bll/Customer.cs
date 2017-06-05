using System;
using System.Collections.Generic;

namespace VideoStore.Bll
{
    public class Customer:IEquatable<Customer>
    {
        public string Name { get; set; }
        public string SocialSecurityNumber { get; set; }

        public bool Equals(Customer other)
        {
            if (other == null)
            {
                return false;
            }
            var result = this.SocialSecurityNumber == other.SocialSecurityNumber;
                         
            return result;
        }
    }
}