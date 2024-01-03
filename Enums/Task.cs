using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SROMCapi.Enums
{
    public enum Task
    {
        start_bot,
        stop_bot,
        start_trace,
        stop_trace,
        set_training_position,
        set_training_radius,
        move_to,
        start_script,
        stop_script
    }
}
