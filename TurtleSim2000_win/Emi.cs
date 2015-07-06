using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurtleSim2000_Linux
{
    class Emi
    {
        string[] emipages;

        //Declare Script Actions:
        string playername = "Turtle";
        string bgchange = "bgchange";
        string charaevent_show_1 = "charaevent show 1";
        string charaevent_show_2 = "charaevent show 2";
        string charaevent_move_1 = "charaevent move 1";
        string charaevent_exit = "charaevent exit";
        string bgscroll = "bgscroll = ";
        string music = "music";
        string music_stop = "music stop";
        string breakpage = "break";
        string Fork = "Fork Question";
        string trigger = "$trigger";

        //Ease of Formatting:
        //use these instead of escapes  Ex. (  emi + "dialogue",  )
        string emi = "Emi: \n\""; 
        string pro = "Turtle: \n";

        bool hasbeenopened = false;


        public Emi()
        {
            hasbeenopened = true;
        }

        string readpage(int line)
        {
            string[] emipages = {
                                   "walk_meetemi",

                                       music,
                                       "Fripperies",

                                       "You get up and decide to go for a walk to >lose some of that fat you keep gaining.",

                                       "bgchange = School_FineArts_court",
                                       "with transition: FadeBlack",

                                       "As you are entering the wooded area where you tend to take your walks.  You spot someone else a bit further ahead.",
                                       "You shrug it off and keep on walking minding your own business.",
                                       "$variable 100 = 100 -r",
                                       "$variable 100 * 700 -r",
                                       "?????:\n\"Hey!  Who are you? $[100] \"",

                                       charaevent_show_1,
                                       "emi/emicas_closedsmile",

                                       music,
                                       "Friendship",

                                       playername + ":\n\"Oh..  Uh..  It's @pro\"",

                                       charaevent_move_1,
                                       "left",

                                       "wait",
                                       "20",

                                       charaevent_move_1,
                                       "right",

                                       "wait",
                                       "40",

                                       charaevent_move_1,
                                       "center",

                                       charaevent_show_1,
                                       "emi/emicas_happy_up",

                                       "S[emi] Hey there!",

                                       "charaevent move 1 = left -2",

                                       emi + "Oh hello, " + playername + ".  My name is Emi!  I see you're out walking around and I though I would join you!\"",

                                       playername + ":\n\"Nah, I think I'm good, but thanks.\"",

                                       charaevent_move_1,
                                       "left",

                                       "OMG! Like, c'mon!",

                                       "bgscroll = -40",

                                       emi + "Oh don't be like that!  C'mon, It'll be fun!  I promise.\"",

                                       "wait",
                                       "5",

                                       charaevent_show_1,
                                       "emi/emicas_wink",

                                       emi  + "Besides, who could turn down an opportunity to walk with someone such as me?\"",
                                       playername + ": \n\"I'm sure there are a few.\"",

                                       charaevent_show_1,
                                       "emi/emicas_weaksmile",

                                       "I guess that was a bit too harsh of me to say.  Oh well, it's too late now.",
                                       "charaevent move 1 = farright -5",
                                       "charaevent show 2 = emi/emicas_pout",
                                       "charaevent move 2 = right -3",
                                       emi + "Do..  Do you not want me to bother you?\"",
                                       "charaevent move 1 = down -2",
                                       "Emi: \n\"C'mon, let me walk with you.  Please?  Don't make me do my pouty face.  No one can resist!\"",

                                       charaevent_show_1,
                                       "emi/emicas_pout",

                                       "charaevent move 1 = left -1",

                                       Fork,
                                       "Oh god!  I was not prepared for that!  How can I turn that down?",
                                       "Say sorry and walk with her",
                                       "walk_meetemi_accept",
                                       "Stutter words and run away.",
                                       "walk_meetemi_decline",
                                       "Fork End",

                                       //================================================MEETEMI-ACCEPT=============================================================
                                       breakpage,

                                       "walk_meetemi_accept",

                                       playername + ": \n\"Ok, we can walk.\"",

                                       charaevent_move_1,
                                       "center",

                                       "charaevent move 1 = up -2",

                                       bgscroll,
                                       "40",

                                       charaevent_show_1,
                                       "emi/emicas_happy_up",

                                       emi + "I told you it always works.\"",

                                       "\"Emi and I start walking together.  She starts to pick up the pace and I slowly follow in suite.  Not so long after I start to get tired and say my goodbye.\"",
                                       "\"She stops me and gives me and we exchange phone numbers real quick.",

                                       charaevent_show_1,
                                       "emi/emicas_neutral",

                                       emi + "I know we just met, but I would really enjoy if you would run with me more often.  It's always nice to have a running partner!\"",

                                       playername + ":\n\"I don't know.  I'm not a big fan of running.\"",

                                       charaevent_show_1,
                                       "emi/emicas_happy_up",

                                       emi + "Just give it a thought and text me if you change your mind! It's fun!  I promise!\"",

                                       charaevent_exit,

                                       "With that she started to run again.  Her pace picked up now that she doesn't have to keep at my pace anymore.\"",

                                       bgchange,
                                       "School_ProDorm_ext",

                                       "I find my way back to the dorms feeling rather accomplished. Also pretty sweaty...  Gross.",

                                       "soundfx",
                                       "doorslam",

                                       bgchange,
                                       "School_ProDorm_bedroom",

                                       music,
                                       "Daylight",

                                       "Man what a day!",

                                       //=============================================MEETEMI-DECLINE=============================================
                                       breakpage,

                                       "walk_meetemi_decline",

                                       playername + ": \n\"I'MSorryButIHaveToGo!\"",

                                       trigger,
                                       "sMetEmi_badend",

                                       "I start to turn around and run away but she grabs my hand just as I'm \nabout to escape her grasp.",

                                       charaevent_show_1,
                                       "emi/emicas_angry",

                                       emi + "What is wrong with you?  Is it something I said?\"",

                                       charaevent_show_1,
                                       "emi/emicas_frown",

                                       emi + "Or do you really have something to go do..\"",
                                       emi + "I'm sorry, you can go if you want to.",

                                       "She looks as if she just did something horribly wrong.  Maybe I should say \nsomething.. but instead I continue on.",

                                       charaevent_exit,

                                       "I start to walk away and I hear her yell from a distance.",

                                       charaevent_show_1,
                                       "emi/emicas_smile",

                                       emi + "If you want to walk with me some other time you know where to find me!",

                                       charaevent_move_1,
                                       "offleft",

                                       "After she yelled from across the opening in the trees she heads off sprinting \nin the other direction.  I turn back around and head back to the dorms.",

                                       bgchange,
                                       "School_ProDorm_bedroom",

                                       music,
                                       "Daylight",

                                       "That was an awkward moment.",

                                       charaevent_exit,

                                       "I shure hope I don't run into her again.",

                                       //============================================ Meeting Emi again (badend) =================================
                                       breakpage,

                                       "walk_meetemi_2",

                                       "You get up and decide to go for a walk to lose some of that fat\n you keep gaining.",

                                       bgchange,
                                       "school_forest1",

                                       "As I'm entering the wooded area where I tend to take my walks.  \nI spot someone else a bit further ahead.",
                                       "I quickly realize it is Emi and I decide to head back in another \nDirection.  She doesn't seem to spot me this time.",
                                       
                                       bgchange,
                                       "school_courtyard",

                                       "I decide to walk around the main campus instead of my favorite spot.  \nNo one really seems to pay me any mind.",
                                       "After about an hour of walking around I decide to head back to my dorm.",

                                       bgchange,
                                       "School_ProDorm_ext",

                                       "I though I just spotted Emi, so I quickly make it up to the front door.",
                                       "After looking back I realize it was just an orange bush in the distance \nI feel slightly relieved, yet stupid at the same time.",

                                       bgchange,
                                       "School_ProDorm_bedroom",


                                       //============================================= Eat With Emi! ==================================================
                                       breakpage, 

                                       "eat_emi",

                                       "I start to feel my stomach rumble, better head to the cafe.",

                                       bgchange,
                                       "school_cafeteria",

                                       "After going through the line and getting my food, I decide to find a nice \nalone spot to gather my thoughts.",
                                       "Just as I'm about to take a seat someone I've seen before comes up to me.",

                                       charaevent_show_1,
                                       "emi/emicas_happy_up",

                                       emi + "Hey " + playername + "!  I knew I would run in to you here!",
                                       playername + ": \n\"Yeah, fancy that!",
                                       "I feel as if what I said was pretty spiteful.\nBut She doesn't seem to be affected by it.",

                                       charaevent_show_1,
                                       "emi/emicas_smile",

                                       emi + "So have you given my offer a thought?\"",
                                       "At first I felt stupified, then I remembered what she was talking about\nBut She must have noticed my face of ignorance.",

                                       charaevent_show_1,
                                       "emi/emicas_frown",

                                       emi + "Did you forget?", 
                                       playername + ": \n\"Uh..  No!  You just caught me off guard is all.\"",

                                       charaevent_show_1,
                                       "emi/emicas_grin",

                                       emi + "Haha!  Don't worry about it!  You have my phone number, just text me\nwhen you have finally decided.\"",
                                       emi + "Or..  I'll text you.\"",

                                       charaevent_show_1,
                                       "emi/emicas_evil_up",

                                       emi + "You don't want me to text you first, haha!\"",
                                       "I kinda felt uncomfortable after that last comment.  I don't know if she is \nkidding, or if she is threatening to kill me.  either way, I should text her \nfirst.\"",

                                       charaevent_show_1,
                                       "emi/emicas_neutral",

                                       "I see her looking around and starts to look fairly concerned.",
                                       emi + "Are you going to eat over here just by yourself?\"",
                                       "The question caught me off gaurd.  She seems to be vary good at that.\"",
                                       playername + ": \n\"Uh, I was--\"",

                                       charaevent_show_1,
                                       "emi/emicas_happy_up",

                                       emi + "I can sit with you!\"",

                                       "She interupted me.  Yet again, off gaurd.",
                                       "I shrug it off and I start to sit down.",

                                       charaevent_show_1,
                                       "emi/emicas_sad",

                                       emi + "Oh!  I forgot I need to be at practice in like two minutes!",

                                       charaevent_move_1,
                                       "offright",

                                       "Before I can even think she is gone.  Oh well, I can finally be alone.",
                                       "I eat my meal and put the tray back up and head back to the dorms.",

                                       charaevent_exit,

                                       bgchange,
                                       "School_ProDorm_ext",

                                       "Just as I am about to enter my dorm I see Emi running with a group\nof people.",
                                       "Huh, she must be on the track team.",

                                       bgchange,
                                       "School_ProDorm_bedroom",

                                       "She kinda scares me.",

                                       //================================================================= Script 003 ===============================================

                                       breakpage,

                                       "emi_movies1",
                                       
                                       playername + ": \n\"" + "Uhh.. Sure.",

                                       charaevent_show_1,
                                       "emi/emicas_happy_up",

                                       emi + "Really?  No one ever wants to go to the movies with me!\"",

                                       charaevent_show_1,
                                       "emi/emicas_awayfrown",

                                       emi + "You're not just teasing me are you?  The last time I got stood up I\ndecked the guy right in his nose.\"",

                                       playername + ": \n\"" + "No, I'm not trying to set you up.\"",

                                       charaevent_show_1,
                                       "emi/emicas_smile_up",

                                       emi + "I'm glad you're not that kind of person.\"",

                                       charaevent_exit,

                                       bgchange,
                                       "School_ProDorm_bedroom",

                                       music,
                                       "Daylight",

                                       "I really don't want to go to the movies, there is never anything I want to\nwatch.  Oh well, at least there will be noise.",

                                       //==================================================== Emi calls you! (first time)  ========================

                                       breakpage,

                                       //should probably show like a picture of a phone here or something..
                                       "emi_calls_one",

                                       "I start to hear a sound I haven't heard in quite some time.",

                                       music,
                                       "Ah_Eh_I_Oh_You",

                                       "RING RING RING!",

                                       "Ugh, that ring startles me everytime!",

                                       "I look at the phone and see that it's Emi that is calling me.",
                                       
                                       Fork,
                                       "Should I answer it?  She is pretty scary..",
                                       "Answer it",
                                       "emi_calls_one_Answer",
                                       "Ignore it",
                                       "emi_calls_one_ignore",
                                       
                                       breakpage,

                                       "emi_calls_one_Answer",

                                       "After a quick thought I decide to answer the call..",

                                       emi + "Hey, " + playername + ", What's taking you so long to make a decision?\"",

                                       pro + "Sorry!  I've just been busy with class!\"",

                                       "That's the best I could come up with, I'm sure she'll buy it.",

                                       emi + "I don't buy it\"",

                                       "Damn!",

                                       emi + "You're just trying to avoid me!  Am I ugly?  Is that it?!",

                                       pro + "Wha--\"",

                                       Fork,
                                       emi + "That's it isn't it?!\"",
                                       "Yes, you're ugly.",
                                       "emi_calls_one_ugly",
                                       "What? No!  I'll walk with you!",
                                       "emi_calls_one_willwalk",

                                       breakpage,

                                       "emi_calls_one_willwalk",

                                       emi + "Alright!  See, I knew you couldn't refuse!\"",
                                       emi + "Now I'd like it if you would meet me in our usual spot at\nleast twice a week.  I'm usually there from 9am to 12pm.\"",
                                       emi + "Have a great day, " + playername + "!\"",

                                       trigger,
                                       "emi_calls_one_good",

                                       "My phone beeps indicating she hung up...",
                                       "Bye?",

                                       music,
                                       "Daylight",

                                       breakpage,

                                       "emi_calls_one_ugly",

                                       "After I regretably say she's ugly I hear nothing from the other line.",
                                       "Then after a pause she speaks up.",
                                       emi + "Is that how you want to play?\"",
                                       pro + "Wha--\"",
                                       emi + "I will get a yes out of you!\"",

                                       trigger,
                                       "emi_calls_one_bad",

                                       "I start to speak but my phone beeps indicating she hung up...",
                                       "Bye?",

                                       music,
                                       "Daylight",

                                       breakpage,

                                       "emi_calls_one_ignore",

                                       "The phone continues to ring until I'm sure it went to my voicemail.",
                                       "I wait for a couple of minutes, but she didn't leave a message.",
                                       "She's weird.",

                                       music,
                                       "Daylight",

                                       //==================================================== Basic Emi Walking Script =========================

                                       breakpage,

                                       "emi_walk_basic",

                                       "You get up and decide to go for a walk to lose some of that fat\n you keep gaining.",

                                       bgchange,
                                       "school_forest1",

                                       "A nice walk around the woods by the campus always clears one's mind.",
                                       "Just as you thought you'd be alone, you see Emi coming your way.",

                                       charaevent_show_1,
                                       "emi/emicas_happy",

                                       emi + "Hey!  I see you made it!!\"",

                                       charaevent_show_1,
                                       "emi/emicas_grit_up",

                                       emi + "Hey!  I see you made it!!   ...  But you're late!\"",

                                       charaevent_show_1,
                                       "emi/emicas_neutral",

                                       charaevent_move_1,
                                       "left",

                                       "There is just no pleaseing this woman!  I decide to shrug it off and we\nstart on a simple walk.  Nothing fancy.",

                                       "#if_greater",
                                       "v0",
                                       "10",
                                       "$jumpto",
                                       "emi_confesseslove",
                                       "#endif",

                                       trigger,
                                       "emi_addheart",

                                       "After close to and hour we decide it's time to call it quites.\nWe go our seperate ways.",

                                       charaevent_exit,

                                       "I make my way back to my dorm reliving the awkward one hour\nwalk of silence.",

                                       bgchange,
                                       "School_ProDorm_bedroom",

                                       "I guess it wasn't too bad..",


                                       //==================================================== testing the debug script =========================

                                       breakpage,

                                       "deleteme",

                                       "Turtle is a fagot.",
                                       "No, really.",

                                       //================================================== Turtle's first ass script ============================

                                       breakpage,

                                       "111",

                                       "asldkfja;lsdkjfal",

                                       charaevent_show_1,
                                       "emi/emicas_angry",

                                       emi + "fuck you and your hair?\"",

                                       "!",
                                       "!"
                                   };
            return emipages[line];
        }

        public string readline(int line)
        {
            //Send proper line to class header

            string Line;

            Line = readpage(line);

            return Line;
        }

    }
}
