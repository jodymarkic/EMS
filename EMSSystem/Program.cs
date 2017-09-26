/*
 *  FILENAME        : Program.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016/08/12
 *  DESCRIPTION     : The file holds the main of the EMSSystem program.
 */

namespace EMSSystem.Presentation
{
    //
    //  CLASS: Program
    //  DESCRIPTION: This class acts as the main entry point for our EMSSystem.
    //               all other methods and classes are called from this point.
    //
    class Program
    {
        //  METHOD      : Main()
        //  DESCRIPTION : Main entry point of application
        //  PARAMETERS  : string[] args
        //  RETURNS     : N/A
        static void Main(string[] args)
        {
            // start the program.
            UIMenu.mainMenu();
        }
    }
}
