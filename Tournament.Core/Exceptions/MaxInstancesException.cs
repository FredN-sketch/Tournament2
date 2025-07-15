using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.Exceptions;

public abstract class MaxInstancesException : Exception
{
    public string Title { get; set; }

    protected MaxInstancesException(string message, string title = "Max instances") : base(message)
    {
        Title = title;
    }
}
public class MaxGamesException : MaxInstancesException
{
    public MaxGamesException(int id) : base($"The tournament with id {id} can't have any more games. The maximum number is {TournamentDetails.MaxGames}.")
    {

    }
}

