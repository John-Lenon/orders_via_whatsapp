﻿namespace Application.Configurations
{
    public class CompanyConnectionStrings
    {
        public CompanyInfo[] List { get; set; }
    }

    public class CompanyInfo
    {
        public string NomeDominio { get; set; }
        public string ConnnectionString { get; set; }
    }
}
