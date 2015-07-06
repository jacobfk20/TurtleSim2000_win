using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurtleSim2000_Linux
{
    class Basic
    {
        //string[] Basicpages= {"!"};
        string playername = "Turtle";
        string bgchange = "bgchange";
        string charaevent_show_1 = "charaevent show 1";
        string charaevent_show_2 = "charaevent show 2";
        string charaevent_move_1 = "charaevent move 1";
        string charaevent_exit = "charaevent exit";
        string music = "music";
        string music_stop = "music stop";
        string breakpage = "break";
        string Fork = "Fork Question";
        string trigger = "$trigger";
        bool hasbeenopened = false;

        string pro = "Turtle: \n\"";

        public Basic()
        {
            hasbeenopened = true;
        }

        string readpage(int line)
        {
            string[] basicpages = {
                                      //------------------Sleep--------------
                                      "sleep",

                                      "// I hate my life.",
                                      "// Turtle can suck a giant floppy dick.",
                                      "\"I guess some sleep would be a wise idea.  It is by far \nmy favorite activity.\"",
                                      "\"No one can bother me in my dreams.  It is magical in my dreams.\"",
                                      "\"Why am I talking to myself? \"",
                                      "I stumble into my bed and start to ponder..",
                                      "...Will I dream?",

                                       breakpage,
                                      
                                       //--------------------------TV--------------------------
                                      "tv",
                                      "\"I do love watching TV.  I hope something is on that is worth it.\"",

                                      breakpage,
                                      
                                      //--------------------------Xbox-------------------------
                                      "xbox",
                                      "I start up the ol' 360, praying that it doesn't red ring on me...",
                                      "...And it didn't!  Yay!",
                                      "You feel yourself drift away in the world of gaming. \nMany hours pass before you feel you've had enough.",

                                      breakpage,
                                     
                                      //-------------------------music--------------------------
                                      "music",
                                      "You pop in your earphones and lose yourself in your weird collection of\nWeird Al and The Trashmen.  Quite a weird collection.",

                                      breakpage,
                                      
                                      //------------------------PornTime------------------------
                                      "porn",
                                      "You finally decide it's time for some 'Me' time.  You grab open your\nlaptop, lay in bed and get down to work.  Dont' worry though,\nIt doesn't take long.",

                                      breakpage,
                                      
                                      //-----------------------Class----------------------------
                                      "class",
                                      "No class today..  Or..  The scripts have not been programmed in yet..  That was sooo meta.",

                                      breakpage,
                                      
                                      //----------------------Homework--------------------------
                                      "homework",
                                      "You sit down and start to do your homework.",
                                      "It takes a bit of work, but you finish up.  A considerable amount of\ntime passes, but you feel happy.",

                                      breakpage,

                                      "nohomework", 
                                      "AINT GOT NONE!",

                                      breakpage,
                                      
                                      //------------------------Text---------------------------
                                      "text",
                                      "Jacob:\nBro!  You have no friends!  Plus they're not programmed in yet anyway.",

                                      breakpage,
                                      
                                      //------------------------Write--------------------------
                                      "write",
                                      "You sit down with the urge to write.  You lose track of time as you write\nline after line.  You finally snap out of it.",

                                      breakpage,
                                      
                                      //------------------------Walk---------------------------
                                      "walk",
                                      "You get up and decide to go for a walk to lose some of that fat\n you keep gaining.",

                                      bgchange,
                                      "school_forest1",

                                      "A nice walk around the woods by the campus always clears one's mind.",

                                      bgchange,
                                      "School_ProDorm_ext",

                                      "I find my way back to the dorms feeling rather accomplished.\nAlso pretty sweaty...  Gross.",

                                      bgchange,
                                      "School_ProDorm_bedroom",
                                      "asdf",

                                      breakpage,
                                      
                                      //----------------------------Eat--------------------------------
                                       "eat",

                                       "You decide to go eat, because food is a must.",

                                       bgchange,
                                       "school_cafeteria",

                                       "You go through the line quickly and you find a seat where no one\nelse is sitting.",
                                       "After quickly gulping down your food you quickly get up and leave.",

                                       bgchange,
                                       "school_dormkenji",

                                       breakpage,
                                      
                                       //---------------------------Win-----------------------------
                                       "win",
                                       "YAY!  YOU WON!  YOU'RE A LONER!  YAAAAAAAY!  ",

                                       "gameover",

                                       breakpage,
                                      
                                       //--------------------------Lose-----------------------------
                                       "lose",

                                       "Well, you lost.  good job.",

                                       "gameover",

                                       //---------------------------- Save Game Script --------------------------
                                       breakpage,
                                       "savegame",

                                       "The game has been saved!",

                                       //---------------------------- Had Class - Just filler -------------------

                                       breakpage,
                                       "hadclass",

                                       "Ughh..",
                                       "I went to class.",
                                       "No really, promise.",

                                       //---------------------------- Intro sequence -----------------------------
                                       breakpage,
                                       //gamestart
                                       "gamestart",

                                       //start with alarm clock buzzing

                                       pro + "Uhh..  I already hate that alarm..",

                                       "I open my eyes and reach over to shut it off.",
                                       "Suddenly I felt lost, as if I didn't know where I was, untill it hit me. I then felt fairly dumb having forgot of my situation.",
                                       "I mean, who could forget their first day finally moving out of the nest? It was quite an emotional day for everyone.",
                                       "It's all rushing back to me as if it is fresh out of the oven now.",

                                       bgchange,
                                       "School_MainBuilding_front",

                                       "My parents and I just reached the front entrance to the school.  My mom was being a typical mother figure, crying and doing other mom stuff.",
                                       "My Dad on the other hand seemed to be looking at some of the other students. He seemed rather astounded by the fashion sense of my generation.",
                                       "My review of my parent's actions were interupted by my mom.  Though I had A tough time focusing on her.",
                                       "Mom: \n\"Are you sure you're going to be alright?\"",
                                       "I didn't really care to think for an answer.  So I just went with the first thing that popped in my mind.",
                                       pro + "Yeah.\"",
                                       "After my response she started asking a million questions, all of which were practicaly the same.",
                                       "Do you know your class schedule?",
                                       "Did you remember to pack all your clothes?",
                                       "Do you know where your classes are?",
                                       "After some time my dad finally decided to speak.",
                                       "Dad: \n\"Don't smother the boy!  He'll do fine.  Plus I can finally have my own room for an office!\"",
                                       "Thanks, dad.",
                                       "They both started argueing on who will get my room.  So many things have made me felt loved over the years.  This by far beats all of those moments.",
                                       "They continue their petty arguement as we continue to the dorms.",

                                       bgchange,
                                       "School_ProDorm_ext",

                                       "The rest seemed to just speed past me quickly.  The next thing I remember is waving bye to them as the walk away from the dorm hall.",
                                       "As I watch them walk away I start to feel that sense of freedom. As of right now, It feels wonderful!",
                                       "I turn around and gracefully make my way back inside.",

                                       bgchange,
                                       "School_ProDorm_bedroom",

                                       "As I look around I realize that I need to put a lot of stuff away still. It's fairly messy in here.  I start to do it, but It's been a pretty long day.  I start to lay down in my bed.",
                                       "I slowly close my eyes and sleep quickly follows.  I'm going to need it, First day of class starts tomorrow.",

                                       //fade to black
                                       //enter dream sequence

                                       "?????: \n\"" + playername + "\"",
                                       "?????: \n\"" + playername + "!!\"",
                                       pro + "WHAT?!\"",
                                       "?????: \n\"You know, you don't have to be rude.\"",
                                       pro + "Who are you I'm trying to dream here!\"",
                                       "?????: \n\"I am...\"",
                                       "Steve: \n\"Steve.\"",
                                       pro + "Good, great to know.\"",
                                       "Steve: \n\"I'm here to show you how to play this game!\"",
                                       pro + "What?  This is how it's going to be done?  You're going to tell the Main character that he is a part of a video game? That's so meta.\"",
                                       "Steve: \n\"...S..Shut up.\"",
                                       "Steve: \n\"Anyway, I'm going to leave you with one of our finest \ndream-game-character-tutorial-guide-girls.(c)\"",
                                       "Steve: \n\"Patent Pending.\"",
                                       pro + "That's a patent that will be worth its weight in lead.\"",

                                       Fork,
                                       "Steve: \n\"Ok, I've been nothing but nice to you!  Do you want my help or not!\"",
                                       "No, I'm pro.",
                                       "gamestart_x",
                                       "I guess, for the girls.",
                                       "gamestart_tut",

                                       //----------------------------------- GameStart (Tutorial) -----------------------------------

                                       breakpage,
                                       "gamestart_tut",

                                       "Steve: \n\"Ok!  Here she comes to teach you the ways of a true turtle!\"",

                                       charaevent_show_1,
                                       "shizu/shizu_adjust_happy_cas",

                                       "Steve: \n\"This is Shizune, she will be your guide.  I will now be\nOn my way!\"",
                                       "I don't really know if he's gone, or if he was even there to begin with.",

                                       charaevent_show_1,
                                       "shizu/shizu_basic_happy_cas",
                                       charaevent_show_1,
                                       "shizu/shizu_basic_normal2_cas",

                                       "She starts waving her hands at me in vary complicated motions. It looks\npretty cool, I'll give her that.",
                                       "After about a minute passes I decide to say something.",
                                       pro + "Hey, what are you doing?\"",

                                       charaevent_show_1,
                                       "shizu/shizu_basic_angry_cas",

                                       Fork,
                                       "She just seems to get angry and move her hands harder.  What should I do?",
                                       "say something else",
                                       "gamestart_stupid",
                                       "Just wake up.",
                                       "gamestart_x",

                                       //----------------------------------- GameStart (stupid) -------------------------------------------
                                       
                                       breakpage,
                                       "gamestart_stupid",

                                       pro + "Why are you doing this??\"",

                                       charaevent_show_1,
                                       "shizu/shizu_cross_angry_cas",

                                       Fork,
                                       "She just seems to get angry and move her hands harder.  What should I do?",
                                       "say something else",
                                       "gamestart_stupid",
                                       "Just wake up.",
                                       "gamestart_x",

                                       //-------------------------------- GameStart (ending) -------------------------------------

                                       breakpage,
                                       "gamestart_x",

                                       "I guess we're done with all that!",

                                       charaevent_exit,

                                       "and now lets start the stupid fucking game.",

                                       trigger,
                                       "gamestart",

                                       //-------------------------------- GameStart (SHORT) -----------------------------------------

                                       breakpage,
                                       "gamestart_short",
                                       
                                       "STARTING GAME! $top",
                                       "This is the short intro.  Everything is ripped out. $clear",

                                       "$trigger : gamestart",

                                       "!",
                                       "!"
                                   };
            return basicpages[line];
        }

        public string readline(int line)
        {


            string Line;

            Line = readpage(line);

            return Line;

        }

    }
}
