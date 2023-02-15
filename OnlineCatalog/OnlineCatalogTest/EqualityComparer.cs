using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OnlineCatalogTests
{
    internal class PersonEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals([AllowNull] Person x, [AllowNull] Person y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Surname == y.Surname;
        }
        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class WorkerEqualityComparer : IEqualityComparer<Worker>
    {
        public bool Equals([AllowNull] Worker x, [AllowNull] Worker y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.PersonId == y.PersonId
                && x.Position == y.Position
                && x.Salary == y.Salary
                && x.HireDate == y.HireDate;
        }
        public int GetHashCode([DisallowNull] Worker obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class LeaderEqualityComparer : IEqualityComparer<Leader>
    {
        public bool Equals([AllowNull] Leader x, [AllowNull] Leader y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.WorkerId == y.WorkerId;
        }
        public int GetHashCode([DisallowNull] Leader obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class SubordinateEqualityComparer : IEqualityComparer<Subordinate>
    {
        public bool Equals([AllowNull] Subordinate x, [AllowNull] Subordinate y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.WorkerId == y.WorkerId
                && x.LeaderId == y.LeaderId;
        }
        public int GetHashCode([DisallowNull] Subordinate obj)
        {
            return obj.GetHashCode();
        }
    }
}
