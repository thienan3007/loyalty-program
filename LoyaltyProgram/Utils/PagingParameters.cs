﻿namespace LoyaltyProgram.Utils
{
    public class PagingParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public string? FilterString { get; set; } = "";
        public int PageSize
        {
            get
            {
                return _pageSize;
            } 
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : (value == 0 ? _pageSize : value);
            }
        }

        public string? OrderBy { get; set; }
    }
}
