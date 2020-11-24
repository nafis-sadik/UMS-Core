﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PagingParam
    {
        private int _page;
        private int _pageSize;
        public int Page
        {
            get => _page;
            set
            {
                _page = value <= 0 ? 1 : value;
            }
        }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value <= 0 ? 10 : value;
            }
        }
        public string SearchString { get; set; }
        public int Skip => _pageSize * (_page - 1);
    }
}
