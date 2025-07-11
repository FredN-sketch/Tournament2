﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Request
{
    public class RequestParams
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(2, int.MaxValue)]
        public int PageSize { get; set; } = 5;
    }
    public class TournamentRequestParams : RequestParams
    {
        public bool IncludeGames { get; set; } = false;
        public bool SortByTitle { get; set; } = false;
    }
    public class GameRequestParams : RequestParams
    {
        public bool SortByTitle { get; set; } = false;
    }
}
