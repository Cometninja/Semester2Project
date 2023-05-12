﻿using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Semester2Prototype
{
    internal class Clue : Sprite
    {
        ClueType _clueType;
        public bool _found = false;

        public Clue(Texture2D image,Vector2 pos,ClueType clueType) :base(image,pos)
        { 
            _clueType = clueType;
            _center = new Vector2(_position.X + 16, _position.Y + 30);
        }

        public override void Update(List<Sprite> sprites)
        {
            _center = new Vector2(_position.X + 16, _position.Y + 30);
            if (!_found)
            {
                _color = Color.Yellow;
            }
            else
            {
                _color = Color.Green;
            }
            base.Update(sprites);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void FoundClue(MessageBox messageBox,Journal journal)
        {
            _found = true;
            journal._cluesFound++;
            switch (_clueType) 
            {
                case ClueType.ChefKnife:
                    journal._goals["FoundKnife"] = true;
                    messageBox.AddMessage("The murder weapon A blood stained 13-inch knife#" +
                        "Its labelled as property of the hotel kitchen#" +
                        "The Murderer must have had access to the area around the kitchen at some point");
                    break;
                case ClueType.MayFlowerPhoto:
                    journal._goals["MsMayflowerPhoto"] = true;
                    messageBox.AddMessage("The photograph is old and worn but an image victim is clearly visible standing next to Ms Mayflower#" +
                        "They seem to be in a relationship#" +
                        "They had Previous ties, a relationship that lead to murder?");
                    break;
                case ClueType.DiscardedClothing:
                    journal._goals["ChangingRoomClue"] = true;
                    messageBox.AddMessage("A pile of discarded staff clothing#" +
                        "There is evidence of small amounts of blood soaked into the clothing#" +
                        "Evidence of the murder trying to cover-up their trail?#" +
                        "There is two identifiable labels' in pile, The Cook and Receptionist");
                    break;
                case ClueType.HotelMasterKey:
                    journal._goals["FoundMaserKey"] = true;
                    messageBox.AddMessage("An old battered key- must be a master key#" +
                        "Why is in on the floor in here though?#" +
                        "A plant, or a quick attempt at hiding it?#" +
                        "Best ask the Cleaner as this is their workspace");
                    break;
                case ClueType.HotelReceptionLogs:
                    journal._goals["HotelReceptionLogs"] = true;
                    messageBox.AddMessage("There are lots of logs of people going in and out of the hotel#" +
                        "there are a couple from yesterday#" +
                        "Mrs Park entered the hotel at 6pm#" +
                        "Mr Sanders & Mr Ross left the hotel at 8pm#" +
                        "Mr Sanders & Mr Ross entered the hotel at 12pm");
                    break;
                case ClueType.KitchenChecks:
                    journal._goals["KitchenChecks"] = true;
                    messageBox.AddMessage("Checks from yesterday's dinners and when they were served#" +
                        "Mr Richards [Victim], Mrs Park, and Ms Mayflower served at 19:00#" +
                        "Mr Montgomery at 22:00");
                    break;
                case ClueType.FinancialDocuments:
                    journal._goals["FinancialDocuments"] = true;
                    messageBox.AddMessage("The desk is a mess with financial reports about the hotel#" +
                        "They don't look good as the hotel has been lossing profit in recent months#" +
                        "A possible motive?");
                    break;
                case ClueType.VictimsDocuments:
                    journal._goals["VictimsDocuments"] = true;
                    messageBox.AddMessage("The Case is filled with financial reports of the hotel, detailing a hotel haemorrhaging money and profit#" +
                        "The documents tell the Victim worked for the company that runs the hotel#" +
                        "What where they here for?");
                    break;
            }
        }
    }
}
