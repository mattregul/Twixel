﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

namespace TwixelAPI
{
    public class Twixel
    {
        public static string clientID = "";
        public static string clientSecret = "";

        string errorString = "";
        public string ErrorString
        {
            get
            {
                return errorString;
            }
        }

        public string accessToken = "";
        public List<Scope> authorizedScopes;

        /// <summary>
        /// The next games url
        /// </summary>
        public WebUrl nextGames;
        public int maxGames;

        /// <summary>
        /// The next streams url
        /// </summary>
        /// <remarks>Used by RetrieveStreams()</remarks>
        public WebUrl nextStreams;

        public int summaryViewers;
        public int summaryChannels;

        public enum Scope
        {
            None,
            UserRead,
            UserBlocksEdit,
            UserBlocksRead,
            UserFollowsEdit,
            ChannelRead,
            ChannelEditor,
            ChannelCommercial,
            ChannelStream,
            ChannelSubscriptions,
            UserSubcriptions,
            ChannelCheckSubscription,
            ChatLogin
        }

        public string gamesString = "{\"_total\":745,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/games/top?limit=10&offset=0\",\"next\":\"https://api.twitch.tv/kraken/games/top?limit=10&offset=10\"},\"top\":[{\"viewers\":117616,\"channels\":1413,\"game\":{\"name\":\"League of Legends\",\"_id\":21779,\"giantbomb_id\":24024,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/League%20of%20Legends-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/League%20of%20Legends-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/League%20of%20Legends-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/League%20of%20Legends-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/League%20of%20Legends-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/League%20of%20Legends-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/League%20of%20Legends-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/League%20of%20Legends-240x144.jpg\"},\"_links\":{}}},{\"viewers\":25998,\"channels\":285,\"game\":{\"name\":\"DayZ\",\"_id\":65632,\"giantbomb_id\":39256,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/DayZ-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/DayZ-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/DayZ-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/DayZ-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/DayZ-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/DayZ-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/DayZ-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/DayZ-240x144.jpg\"},\"_links\":{}}},{\"viewers\":24393,\"channels\":224,\"game\":{\"name\":\"Hearthstone: Heroes of Warcraft\",\"_id\":138585,\"giantbomb_id\":42033,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/Hearthstone%3A%20Heroes%20of%20Warcraft-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/Hearthstone%3A%20Heroes%20of%20Warcraft-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/Hearthstone%3A%20Heroes%20of%20Warcraft-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/Hearthstone%3A%20Heroes%20of%20Warcraft-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/Hearthstone%3A%20Heroes%20of%20Warcraft-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/Hearthstone%3A%20Heroes%20of%20Warcraft-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/Hearthstone%3A%20Heroes%20of%20Warcraft-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/Hearthstone%3A%20Heroes%20of%20Warcraft-240x144.jpg\"},\"_links\":{}}},{\"viewers\":13795,\"channels\":492,\"game\":{\"name\":\"Call of Duty: Ghosts\",\"_id\":118200,\"giantbomb_id\":41520,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/Call%20of%20Duty%3A%20Ghosts-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/Call%20of%20Duty%3A%20Ghosts-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/Call%20of%20Duty%3A%20Ghosts-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/Call%20of%20Duty%3A%20Ghosts-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/Call%20of%20Duty%3A%20Ghosts-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/Call%20of%20Duty%3A%20Ghosts-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/Call%20of%20Duty%3A%20Ghosts-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/Call%20of%20Duty%3A%20Ghosts-240x144.jpg\"},\"_links\":{}}},{\"viewers\":12563,\"channels\":9,\"game\":{\"name\":\"Warcraft III: The Frozen Throne\",\"_id\":12924,\"giantbomb_id\":14073,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/Warcraft%20III%3A%20The%20Frozen%20Throne-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/Warcraft%20III%3A%20The%20Frozen%20Throne-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/Warcraft%20III%3A%20The%20Frozen%20Throne-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/Warcraft%20III%3A%20The%20Frozen%20Throne-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/Warcraft%20III%3A%20The%20Frozen%20Throne-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/Warcraft%20III%3A%20The%20Frozen%20Throne-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/Warcraft%20III%3A%20The%20Frozen%20Throne-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/Warcraft%20III%3A%20The%20Frozen%20Throne-240x144.jpg\"},\"_links\":{}}},{\"viewers\":12381,\"channels\":164,\"game\":{\"name\":\"Dota 2\",\"_id\":29595,\"giantbomb_id\":32887,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/Dota%202-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/Dota%202-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/Dota%202-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/Dota%202-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/Dota%202-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/Dota%202-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/Dota%202-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/Dota%202-240x144.jpg\"},\"_links\":{}}},{\"viewers\":9858,\"channels\":386,\"game\":{\"name\":\"World of Warcraft: Mists of Pandaria\",\"_id\":32954,\"giantbomb_id\":36734,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/World%20of%20Warcraft%3A%20Mists%20of%20Pandaria-240x144.jpg\"},\"_links\":{}}},{\"viewers\":8332,\"channels\":545,\"game\":{\"name\":\"Minecraft\",\"_id\":27471,\"giantbomb_id\":30475,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/Minecraft-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/Minecraft-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/Minecraft-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/Minecraft-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/Minecraft-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/Minecraft-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/Minecraft-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/Minecraft-240x144.jpg\"},\"_links\":{}}},{\"viewers\":8276,\"channels\":112,\"game\":{\"name\":\"StarCraft II: Heart of the Swarm\",\"_id\":21818,\"giantbomb_id\":24078,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/StarCraft%20II%3A%20Heart%20of%20the%20Swarm-240x144.jpg\"},\"_links\":{}}},{\"viewers\":7418,\"channels\":177,\"game\":{\"name\":\"Counter-Strike: Global Offensive\",\"_id\":32399,\"giantbomb_id\":36113,\"box\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-boxart/Counter-Strike%3A%20Global%20Offensive-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-boxart/Counter-Strike%3A%20Global%20Offensive-52x72.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-boxart/Counter-Strike%3A%20Global%20Offensive-136x190.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-boxart/Counter-Strike%3A%20Global%20Offensive-272x380.jpg\"},\"logo\":{\"template\":\"http://static-cdn.jtvnw.net/ttv-logoart/Counter-Strike%3A%20Global%20Offensive-{width}x{height}.jpg\",\"small\":\"http://static-cdn.jtvnw.net/ttv-logoart/Counter-Strike%3A%20Global%20Offensive-60x36.jpg\",\"medium\":\"http://static-cdn.jtvnw.net/ttv-logoart/Counter-Strike%3A%20Global%20Offensive-120x72.jpg\",\"large\":\"http://static-cdn.jtvnw.net/ttv-logoart/Counter-Strike%3A%20Global%20Offensive-240x144.jpg\"},\"_links\":{}}}]}";
        public string streamsString = "{\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams?game=League+of+Legends&limit=25&offset=0\",\"featured\":\"https://api.twitch.tv/kraken/streams/featured\",\"summary\":\"https://api.twitch.tv/kraken/streams/summary\",\"followed\":\"https://api.twitch.tv/kraken/streams/followed\",\"next\":\"https://api.twitch.tv/kraken/streams?game=League+of+Legends&limit=25&offset=25\"},\"streams\":[{\"name\":\"live_user_tsm_bjergsen\",\"broadcaster\":\"obs\",\"_id\":8037187600,\"game\":\"League of Legends\",\"viewers\":30533,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_tsm_bjergsen-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/tsm_bjergsen\"},\"channel\":{\"mature\":null,\"status\":\"Team SoloMid Bjergsen - Mid lane S4! \",\"display_name\":\"TSM_Bjergsen\",\"game\":\"League of Legends\",\"_id\":38421618,\"name\":\"tsm_bjergsen\",\"created_at\":\"2012-12-12T18:51:04Z\",\"updated_at\":\"2013-12-30T00:28:47Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/tsm_bjergsen-profile_image-3891e3c054d25c36-300x300.png\",\"banner\":null,\"video_banner\":null,\"background\":null,\"url\":\"http://www.twitch.tv/tsm_bjergsen\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen\",\"follows\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/tsm_bjergsen\",\"features\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/tsm_bjergsen/videos\"},\"teams\":[{\"_id\":272,\"name\":\"solomid\",\"info\":\"\\n\",\"display_name\":\"SoloMid\",\"created_at\":\"2012-04-28T23:02:22Z\",\"updated_at\":\"2013-05-24T00:17:33Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-solomid-team_logo_image-2c7087c64d6c6f51-300x300.jpeg\",\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/solomid\"}}]}},{\"name\":\"live_user_tsm_theoddone\",\"broadcaster\":\"delay\",\"_id\":8033865024,\"game\":\"League of Legends\",\"viewers\":23842,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_tsm_theoddone-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/tsm_theoddone\"},\"channel\":{\"mature\":false,\"status\":\"TSM Snapdragon TheOddOne Jungle Power Hour\",\"display_name\":\"TSM_TheOddOne\",\"game\":\"League of Legends\",\"_id\":30080840,\"name\":\"tsm_theoddone\",\"created_at\":\"2012-04-27T01:42:54Z\",\"updated_at\":\"2013-12-30T02:04:03Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/tsm_theoddone-profile_image-a74433a8f8ced577-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/tsm_theoddone-channel_header_image-81b7a9928262184b-640x125.png\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/tsm_theoddone-channel_offline_image-cbac951bb25f73aa-640x360.png\",\"background\":null,\"url\":\"http://www.twitch.tv/tsm_theoddone\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone\",\"follows\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/tsm_theoddone\",\"features\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/tsm_theoddone/videos\"},\"teams\":[{\"_id\":272,\"name\":\"solomid\",\"info\":\"\\n\",\"display_name\":\"SoloMid\",\"created_at\":\"2012-04-28T23:02:22Z\",\"updated_at\":\"2013-05-24T00:17:33Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-solomid-team_logo_image-2c7087c64d6c6f51-300x300.jpeg\",\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/solomid\"}}]}},{\"name\":\"live_user_sky_mp3\",\"broadcaster\":\"obs\",\"_id\":8037228144,\"game\":\"League of Legends\",\"viewers\":7953,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_sky_mp3-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/sky_mp3\"},\"channel\":{\"mature\":null,\"status\":\"PATH TO DIAMOND I IS A GO.\",\"display_name\":\"Sky_mp3\",\"game\":\"League of Legends\",\"_id\":38876314,\"name\":\"sky_mp3\",\"created_at\":\"2012-12-30T03:07:17Z\",\"updated_at\":\"2013-12-30T00:31:35Z\",\"logo\":null,\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/sky_mp3-channel_offline_image-1480247f2eef0edc-640x360.png\",\"background\":null,\"url\":\"http://www.twitch.tv/sky_mp3\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/sky_mp3\",\"follows\":\"https://api.twitch.tv/kraken/channels/sky_mp3/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/sky_mp3/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/sky_mp3/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/sky_mp3\",\"features\":\"https://api.twitch.tv/kraken/channels/sky_mp3/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/sky_mp3/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/sky_mp3/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/sky_mp3/videos\"},\"teams\":[]}},{\"name\":\"live_user_yeetz\",\"broadcaster\":\"obs\",\"_id\":8038646496,\"game\":\"League of Legends\",\"viewers\":6578,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_yeetz-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/yeetz\"},\"channel\":{\"mature\":null,\"status\":\"yeTz  jogando na smurf e dps duo com o digo\\r\\n\",\"display_name\":\"yeeTz\",\"game\":\"League of Legends\",\"_id\":27680990,\"name\":\"yeetz\",\"created_at\":\"2012-01-23T01:43:33Z\",\"updated_at\":\"2013-12-30T02:54:55Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/yeetz-profile_image-5487191f0c8e2b2a-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/yeetz-channel_header_image-4ada64ee67539e3d-640x125.jpeg\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/yeetz-channel_offline_image-82eb02dd340c72d6-640x360.jpeg\",\"background\":null,\"url\":\"http://www.twitch.tv/yeetz\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/yeetz\",\"follows\":\"https://api.twitch.tv/kraken/channels/yeetz/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/yeetz/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/yeetz/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/yeetz\",\"features\":\"https://api.twitch.tv/kraken/channels/yeetz/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/yeetz/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/yeetz/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/yeetz/videos\"},\"teams\":[]}},{\"name\":\"live_user_wingsofdeath\",\"broadcaster\":\"obs\",\"_id\":8035222896,\"game\":\"League of Legends\",\"viewers\":5059,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_wingsofdeath-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/wingsofdeath\"},\"channel\":{\"mature\":false,\"status\":\"Wingsofdeath Most Consistent Player NA Kappa. Commentary and Christmas Cookies!\",\"display_name\":\"Wingsofdeath\",\"game\":\"League of Legends\",\"_id\":30171560,\"name\":\"wingsofdeath\",\"created_at\":\"2012-04-30T06:48:32Z\",\"updated_at\":\"2013-12-29T21:29:12Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/wingsofdeath-profile_image-4e3d5c1051d7561a-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/wingsofdeath-channel_header_image-f6b7f7e8b87ab953-640x125.png\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/wingsofdeath-channel_offline_image-7a8ee8e4f1b2e2bc-640x360.png\",\"background\":null,\"url\":\"http://www.twitch.tv/wingsofdeath\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/wingsofdeath\",\"follows\":\"https://api.twitch.tv/kraken/channels/wingsofdeath/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/wingsofdeath/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/wingsofdeath/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/wingsofdeath\",\"features\":\"https://api.twitch.tv/kraken/channels/wingsofdeath/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/wingsofdeath/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/wingsofdeath/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/wingsofdeath/videos\"},\"teams\":[{\"_id\":272,\"name\":\"solomid\",\"info\":\"\\n\",\"display_name\":\"SoloMid\",\"created_at\":\"2012-04-28T23:02:22Z\",\"updated_at\":\"2013-05-24T00:17:33Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-solomid-team_logo_image-2c7087c64d6c6f51-300x300.jpeg\",\"banner\":null,\"background\":null,\"_links\":" + 
        "{\"self\":\"https://api.twitch.tv/kraken/teams/solomid\"}}]}},{\"name\":\"live_user_trick2g\",\"broadcaster\":\"xsplit\",\"_id\":8035294192,\"game\":\"League of Legends\",\"viewers\":4988,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_trick2g-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/trick2g\"},\"channel\":{\"mature\":true,\"status\":\"Backdooring ATM b4 subwars\",\"display_name\":\"Trick2g\",\"game\":\"League of Legends\",\"_id\":28036688,\"name\":\"trick2g\",\"created_at\":\"2012-02-06T21:16:52Z\",\"updated_at\":\"2013-12-30T02:44:55Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/trick2g-profile_image-4f7802d5130b20e9-300x300.png\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/trick2g-channel_header_image-a712bd1af57ae2ee-640x125.jpeg\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/trick2g-channel_offline_image-d525e160b1d49115-640x360.png\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/trick2g-background_image-39fff00c8fff4205.jpeg\",\"url\":\"http://www.twitch.tv/trick2g\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/trick2g\",\"follows\":\"https://api.twitch.tv/kraken/channels/trick2g/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/trick2g/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/trick2g/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/trick2g\",\"features\":\"https://api.twitch.tv/kraken/channels/trick2g/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/trick2g/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/trick2g/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/trick2g/videos\"},\"teams\":[]}},{\"name\":\"live_user_kaceytron\",\"broadcaster\":\"obs\",\"_id\":8037495808,\"game\":\"League of Legends\",\"viewers\":3134,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_kaceytron-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/kaceytron\"},\"channel\":{\"mature\":true,\"status\":\"quest for pentakill\",\"display_name\":\"kaceytron\",\"game\":\"League of Legends\",\"_id\":30281925,\"name\":\"kaceytron\",\"created_at\":\"2012-05-05T06:46:13Z\",\"updated_at\":\"2013-12-30T01:54:16Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/kaceytron-profile_image-c3edac9cfbbcd67d-300x300.jpeg\",\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/kaceytron-channel_offline_image-4af5c9593e07603a-640x360.jpeg\",\"background\":null,\"url\":\"http://www.twitch.tv/kaceytron\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/kaceytron\",\"follows\":\"https://api.twitch.tv/kraken/channels/kaceytron/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/kaceytron/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/kaceytron/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/kaceytron\",\"features\":\"https://api.twitch.tv/kraken/channels/kaceytron/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/kaceytron/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/kaceytron/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/kaceytron/videos\"},\"teams\":[{\"_id\":167,\"name\":\"bloodlegion\",\"info\":\"Only the best Serious Gaming channels on Twitch.tv!\\n\\n\\n\\n\",\"display_name\":\"Serious Gaming\",\"created_at\":\"2012-01-26T00:42:24Z\",\"updated_at\":\"2013-11-14T04:58:28Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-bloodlegion-team_logo_image-3d86bda4dcc9ecb4-300x300.png\",\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/bloodlegion\"}}]}},{\"name\":\"live_user_thepvpremade\",\"broadcaster\":\"xsplit\",\"_id\":8038752704,\"game\":\"League of Legends\",\"viewers\":2918,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_thepvpremade-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/thepvpremade\"},\"channel\":{\"mature\":false,\"status\":\"League of Legends - Bronze V\",\"display_name\":\"ThePvPremade\",\"game\":\"League of Legends\",\"_id\":44141636,\"name\":\"thepvpremade\",\"created_at\":\"2013-05-30T19:09:04Z\",\"updated_at\":\"2013-12-30T03:05:35Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/thepvpremade-profile_image-6c0780612eb9c7a5-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/thepvpremade-channel_header_image-a4b5b5beb4a5c81d-640x125.png\",\"video_banner\":null,\"background\":null,\"url\":\"http://www.twitch.tv/thepvpremade\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/thepvpremade\",\"follows\":\"https://api.twitch.tv/kraken/channels/thepvpremade/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/thepvpremade/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/thepvpremade/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/thepvpremade\",\"features\":\"https://api.twitch.tv/kraken/channels/thepvpremade/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/thepvpremade/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/thepvpremade/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/thepvpremade/videos\"},\"teams\":[]}},{\"name\":\"live_user_bischulol\",\"broadcaster\":\"obs\",\"_id\":8037443824,\"game\":\"League of Legends\",\"viewers\":2537,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_bischulol-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/bischulol\"},\"channel\":{\"mature\":null,\"status\":\"Bischu! Can't stop playing Yasuo pls save.\",\"display_name\":\"Bischulol\",\"game\":\"League of Legends\",\"_id\":35671232,\"name\":\"bischulol\",\"created_at\":\"2012-08-24T13:18:31Z\",\"updated_at\":\"2013-12-30T00:52:04Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/bischulol-profile_image-11b3b0deaa4dcc90-300x300.png\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/bischulol-channel_header_image-69e782044696216d-640x125.jpeg\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/bischulol-channel_offline_image-ba655a22ebbe543f-640x360.jpeg\",\"background\":null,\"url\":\"http://www.twitch.tv/bischulol\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/bischulol\",\"follows\":\"https://api.twitch.tv/kraken/channels/bischulol/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/bischulol/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/bischulol/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/bischulol\",\"features\":\"https://api.twitch.tv/kraken/channels/bischulol/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/bischulol/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/bischulol/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/bischulol/videos\"},\"teams\":[{\"_id\":272,\"name\":\"solomid\",\"info\":\"\\n\",\"display_name\":\"SoloMid\",\"created_at\":\"2012-04-28T23:02:22Z\",\"updated_at\":\"2013-05-24T00:17:33Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-solomid-team_logo_image-2c7087c64d6c6f51-300x300.jpeg\",\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/solomid\"}}]}},{\"name\":\"live_user_znipes\",\"broadcaster\":\"obs\",\"_id\":8033432944,\"game\":\"League of Legends\",\"viewers\":1866,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_znipes-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/znipes\"},\"channel\":{\"mature\":null,\"status\":\"BRONZE 5 TO DIAMOND 1 - Day 11 Twitch for Twitch.tv\",\"display_name\":\"Znipes\",\"game\":\"League of Legends\",\"_id\":53229363,\"name\":\"znipes\",\"created_at\":\"2013-12-14T19:44:29Z\",\"updated_at\":\"2013-12-29T20:09:41Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/znipes-profile_image-9139269eb66f9edb-300x300.jpeg\",\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/znipes-channel_offline_image-f4df395e3fc73c31-640x360.png\",\"background\":null,\"url\":\"http://www.twitch.tv/znipes\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/znipes\",\"follows\":\"https://api.twitch.tv/kraken/channels/znipes/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/znipes/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/znipes/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/znipes\",\"features\":\"https://api.twitch.tv/kraken/channels/znipes/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/znipes/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/znipes/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/znipes/videos\"},\"teams\":[]}},{\"name\":\"live_user_hail9\",\"broadcaster\":\"obs\",\"_id\":8037070656,\"game\":\"League of Legends\",\"viewers\":1598,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_hail9-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/hail9\"},\"channel\":" + 
        "{\"mature\":false,\"status\":\"C9 HyperX Hai~~\",\"display_name\":\"HaiL9\",\"game\":\"League of Legends\",\"_id\":28631766,\"name\":\"hail9\",\"created_at\":\"2012-03-01T16:39:37Z\",\"updated_at\":\"2013-12-30T02:28:40Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/hail9-profile_image-303e525202f2fcfe-300x300.jpeg\",\"banner\":null,\"video_banner\":null,\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/hail9-background_image-471b076f39b08467.png\",\"url\":\"http://www.twitch.tv/hail9\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/hail9\",\"follows\":\"https://api.twitch.tv/kraken/channels/hail9/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/hail9/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/hail9/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/hail9\",\"features\":\"https://api.twitch.tv/kraken/channels/hail9/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/hail9/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/hail9/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/hail9/videos\"},\"teams\":[{\"_id\":272,\"name\":\"solomid\",\"info\":\"\\n\",\"display_name\":\"SoloMid\",\"created_at\":\"2012-04-28T23:02:22Z\",\"updated_at\":\"2013-05-24T00:17:33Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-solomid-team_logo_image-2c7087c64d6c6f51-300x300.jpeg\",\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/solomid\"}},{\"_id\":735,\"name\":\"cloud9\",\"info\":\"\\n\",\"display_name\":\"Cloud 9 HyperX\",\"created_at\":\"2013-07-16T02:18:31Z\",\"updated_at\":\"2013-12-28T22:43:05Z\",\"logo\":null,\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/cloud9\"}}]}},{\"name\":\"live_user_pokelawls\",\"broadcaster\":\"obs\",\"_id\":8037508976,\"game\":\"League of Legends\",\"viewers\":1382,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_pokelawls-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/pokelawls\"},\"channel\":{\"mature\":false,\"status\":\"Jinxin my way to challenger. (D1 ATM) | PRESIDENT OF LOL - Diamond 1 SoloQ\",\"display_name\":\"Pokelawls\",\"game\":\"League of Legends\",\"_id\":12943173,\"name\":\"pokelawls\",\"created_at\":\"2010-06-05T09:10:22Z\",\"updated_at\":\"2013-12-30T00:57:39Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/pokelawls-profile_image-8b433df86c9b2115-300x300.png\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/pokelawls-channel_header_image-bfe49568d4efa7ec-640x125.png\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/pokelawls-channel_offline_image-f8d97fc15a428e49-640x360.png\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/pokelawls-background_image-7845dbef474dfbbf.jpeg\",\"url\":\"http://www.twitch.tv/pokelawls\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/pokelawls\",\"follows\":\"https://api.twitch.tv/kraken/channels/pokelawls/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/pokelawls/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/pokelawls/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/pokelawls\",\"features\":\"https://api.twitch.tv/kraken/channels/pokelawls/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/pokelawls/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/pokelawls/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/pokelawls/videos\"},\"teams\":[]}},{\"name\":\"live_user_espeonbot\",\"broadcaster\":\"obs\",\"_id\":8036342352,\"game\":\"League of Legends\",\"viewers\":1389,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_espeonbot-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/espeonbot\"},\"channel\":{\"mature\":false,\"status\":\"Espeon -  Utility Carrier x.o\",\"display_name\":\"Espeonbot\",\"game\":\"League of Legends\",\"_id\":35946395,\"name\":\"espeonbot\",\"created_at\":\"2012-09-04T00:35:58Z\",\"updated_at\":\"2013-12-29T23:07:30Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/espeonbot-profile_image-aabe7f4bbfdc1d52-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/espeonbot-channel_header_image-2f3e5913b3fd9cad-640x125.png\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/espeonbot-channel_offline_image-c3d83324911c9578-640x360.png\",\"background\":null,\"url\":\"http://www.twitch.tv/espeonbot\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/espeonbot\",\"follows\":\"https://api.twitch.tv/kraken/channels/espeonbot/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/espeonbot/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/espeonbot/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/espeonbot\",\"features\":\"https://api.twitch.tv/kraken/channels/espeonbot/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/espeonbot/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/espeonbot/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/espeonbot/videos\"},\"teams\":[]}},{\"name\":\"live_user_iijeriichoii\",\"broadcaster\":\"xsplit\",\"_id\":8038214480,\"game\":\"League of Legends\",\"viewers\":1237,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_iijeriichoii-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/iijeriichoii\"},\"channel\":{\"mature\":null,\"status\":\"1v1 me br0\",\"display_name\":\"iijeriichoii\",\"game\":\"League of Legends\",\"_id\":10397006,\"name\":\"iijeriichoii\",\"created_at\":\"2010-02-06T01:00:07Z\",\"updated_at\":\"2013-12-30T02:46:09Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/iijeriichoii-profile_image-b9d9c204f19237f2-300x300.png\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/iijeriichoii-channel_header_image-7ab6721f12a41861-640x125.png\",\"video_banner\":null,\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/iijeriichoii-background_image-7481b97dfd722398.png\",\"url\":\"http://www.twitch.tv/iijeriichoii\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/iijeriichoii\",\"follows\":\"https://api.twitch.tv/kraken/channels/iijeriichoii/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/iijeriichoii/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/iijeriichoii/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/iijeriichoii\",\"features\":\"https://api.twitch.tv/kraken/channels/iijeriichoii/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/iijeriichoii/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/iijeriichoii/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/iijeriichoii/videos\"},\"teams\":[{\"_id\":325,\"name\":\"gamershore\",\"info\":\"We're those sexy dudes from LA who play video games.\\n\\n\",\"display_name\":\"GamerShore\",\"created_at\":\"2012-06-16T08:23:14Z\",\"updated_at\":\"2013-05-24T00:17:32Z\",\"logo\":null,\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/gamershore\"}}]}},{\"name\":\"live_user_shiphtur\",\"broadcaster\":\"obs\",\"_id\":8037027504,\"game\":\"League of Legends\",\"viewers\":1181,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_shiphtur-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/shiphtur\"},\"channel\":{\"mature\":false,\"status\":\"Coast Shiphtur - Rank 2 Challenger Mid\",\"display_name\":\"Shiphtur\",\"game\":\"League of Legends\",\"_id\":26560695,\"name\":\"shiphtur\",\"created_at\":\"2011-12-04T07:21:37Z\",\"updated_at\":\"2013-12-30T00:11:45Z\",\"logo\":null,\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/shiphtur-channel_offline_image-630f62d23012068e-640x360.png\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/shiphtur-background_image-bfec14b3d5a8cc7d.jpeg\",\"url\":\"http://www.twitch.tv/shiphtur\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/shiphtur\",\"follows\":\"https://api.twitch.tv/kraken/channels/shiphtur/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/shiphtur/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/shiphtur/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/shiphtur\",\"features\":\"https://api.twitch.tv/kraken/channels/shiphtur/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/shiphtur/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/shiphtur/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/shiphtur/videos\"},\"teams\":[{\"_id\":807,\"name\":\"teamcoast\",\"info\":\"Team Coast is a North American League Championship Series League of Legends professional gaming team. Visit  our web site: www.teamcoast.net. Like us on Facebook.com/teamcoastgaming. Follow us on Twitter @teamcoastgaming\\n\\n\",\"display_name\":\"Team Coast\",\"created_at\":\"2013-09-24T19:12:44Z\",\"updated_at\":\"2013-12-22T13:13:27Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-teamcoast-team_logo_image-b7f0125fdf068cdf-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-teamcoast-banner_image-114098c9f2d9b2a1-640x125.png\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/team-teamcoast-background_image-f5ffd18acd19fb58.jpeg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/teamcoast\"}}]}},{\"name\":\"live_user_16k_swe\",\"broadcaster\":\"obs\",\"_id\":8034828544,\"game\":\"League of Legends\",\"viewers\":1112,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_16k_swe-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/16k_swe\"},\"channel\":{\"mature\":false,\"status\":\"【MasterOfNidalee - bronze to diamond marathoon v2】\",\"display_name\":\"16k_swe\",\"game\":\"League of Legends\",\"_id\":45810111,\"name\":\"16k_swe\",\"created_at\":\"2013-07-08T16:26:22Z\",\"updated_at\":\"2013-12-29T21:03:11Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/16k_swe-profile_image-ebbabe8eee1d081d-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/16k_swe-channel_header_image-36c9be38ea753509-640x125.jpeg\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/16k_swe-channel_offline_image-3ad5b17d9d4a1f58-640x360.jpeg\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/16k_swe-background_image-22949a1fecdc5fee.jpeg\",\"url\":\"http://www.twitch.tv/16k_swe\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/16k_swe\",\"follows\":\"https://api.twitch.tv/kraken/channels/16k_swe/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/16k_swe/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/16k_swe/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/16k_swe\",\"features\":\"https://api.twitch.tv/kraken/channels/16k_swe/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/16k_swe/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/16k_swe/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/16k_swe/videos\"},\"teams\":[]}},{\"name\":\"live_user_topshowlol\",\"broadcaster\":\"obs\",\"_id\":8037582224,\"game\":\"League of Legends\",\"viewers\":675,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_topshowlol-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/topshowlol\"},\"channel\":{\"mature\":false,\"status\":\"菁英(Challenger) TopShow 早安喔\",\"display_name\":\"topshowlol\",\"game\":\"League of Legends\",\"_id\":37507751,\"name\":\"topshowlol\",\"created_at\":\"2012-11-07T16:53:36Z\",\"updated_at\":\"2013-12-30T01:04:39Z\",\"logo\":null,\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/topshowlol-channel_offline_image-c96226b73109b631-640x360.jpeg\",\"background\":null,\"url\":\"http://www.twitch.tv/topshowlol\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/topshowlol\",\"follows\":\"https://api.twitch.tv/kraken/channels/topshowlol/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/topshowlol/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/topshowlol/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/topshowlol\",\"features\":\"https://api.twitch.tv/kraken/channels/topshowlol/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/topshowlol/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/topshowlol/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/topshowlol/videos\"},\"teams\":[]}},{\"name\":\"live_user_theemero\",\"broadcaster\":\"obs\",\"_id\":8032306528,\"game\":\"League of Legends\",\"viewers\":602,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_theemero-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/theemero\"},\"channel\":{\"mature\":null,\"status\":\"[GER] Emero - LoL DuoQ - Kein Weihnachten, kein Stress.\",\"display_name\":\"TheEmero\",\"game\":\"League of Legends\",\"_id\":9652911,\"name\":\"theemero\",\"created_at\":\"2009-12-29T17:48:17Z\",\"updated_at\":\"2013-12-29T17:14:41Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/theemero-profile_image-272133e59eaddfac-300x300.jpeg\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/theemero-channel_header_image-db83811f35d5a332-640x125.png\",\"video_banner\":null,\"background\":null,\"url\":\"http://www.twitch.tv/theemero\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/theemero\",\"follows\":\"https://api.twitch.tv/kraken/channels/theemero/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/theemero/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/theemero/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/theemero\",\"features\":\"https://api.twitch.tv/kraken/channels/theemero/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/theemero/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/theemero/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/theemero/videos\"},\"teams\":[{\"_id\":532,\"name\":\"mym\",\"info\":\"\\n\",\"display_name\":\"Meet Your Makers\",\"created_at\":\"2013-01-15T22:38:54Z\",\"updated_at\":\"2013-05-24T00:18:02Z\",\"logo\":null,\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/mym\"}},{\"_id\":693,\"name\":\"livelp\",\"info\":\"\\n\",\"display_name\":\"liveLP\",\"created_at\":\"2013-06-06T23:13:34Z\",\"updated_at\":\"2013-06-06T23:17:11Z\",\"logo\":null,\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/livelp\"}}]}},{\"name\":\"live_user_machinima\",\"broadcaster\":\"obs\",\"_id\":8038153616,\"game\":\"League of Legends\",\"viewers\":578,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_machinima-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/machinima\"},\"channel\":{\"mature\":false,\"status\":\"TSM XPECIAL AND C9 BALLS POWER HOUR\",\"display_name\":\"Machinima\",\"game\":\"League of Legends\",\"_id\":49161847,\"name\":\"machinima\",\"created_at\":\"2013-09-18T03:49:54Z\",\"updated_at\":\"2013-12-30T02:07:30Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/machinima-profile_image-69cd2c50320f19a6-300x300.png\",\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/machinima-channel_offline_image-7493329f0bea45d1-640x360.png\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/machinima-background_image-bb710b7fddc7b6d4.png\",\"url\":\"http://www.twitch.tv/machinima\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/machinima\",\"follows\":\"https://api.twitch.tv/kraken/channels/machinima/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/machinima/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/machinima/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/machinima\",\"features\":\"https://api.twitch.tv/kraken/channels/machinima/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/machinima/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/machinima/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/machinima/videos\"},\"teams\":[]}},{\"name\":\"live_user_frozen333\",\"broadcaster\":\"obs\",\"_id\":8036319696,\"game\":\"League of Legends\",\"viewers\":574,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_frozen333-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/frozen333\"},\"channel\":{\"mature\":null,\"status\":\"Viewer Games NA / EUW ! Giveaway @ 2400! AMA in chat. What does CILTS meannnn?\",\"display_name\":\"Frozen333\",\"game\":\"League of Legends\",\"_id\":38733552,\"name\":\"frozen333\",\"created_at\":\"2012-12-24T14:06:04Z\",\"updated_at\":\"2013-12-30T03:07:15Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/frozen333-profile_image-868a8617eb6b9aa7-300x300.jpeg\",\"banner\":null,\"video_banner\":null,\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/frozen333-background_image-82d196986ffd86d3.png\",\"url\":\"http://www.twitch.tv/frozen333\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/frozen333\",\"follows\":\"https://api.twitch.tv/kraken/channels/frozen333/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/frozen333/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/frozen333/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/frozen333\",\"features\":\"https://api.twitch.tv/kraken/channels/frozen333/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/frozen333/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/frozen333/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/frozen333/videos\"},\"teams\":[]}},{\"name\":\"live_user_loldubr\",\"broadcaster\":\"obs\",\"_id\":8038554688,\"game\":\"League of Legends\",\"viewers\":503,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_loldubr-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/loldubr\"},\"channel\":{\"mature\":null,\"status\":\"LoLDuBR ON - League Of Legends AMA - Ask me Anything c/ Celu!\",\"display_name\":\"LoLDuBR\",\"game\":\"League of Legends\",\"_id\":32965998,\"name\":\"loldubr\",\"created_at\":\"2012-08-12T02:07:07Z\",\"updated_at\":\"2013-12-30T02:44:41Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/loldubr-profile_image-91bcdba59a85a838-300x300.png\",\"banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/loldubr-channel_header_image-9c18d83293a5c8cb-640x125.png\",\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/loldubr-channel_offline_image-a8601b0278a776c5-640x360.png\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/loldubr-background_image-0101b5618f55f77a.jpeg\",\"url\":\"http://www.twitch.tv/loldubr\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/loldubr\",\"follows\":\"https://api.twitch.tv/kraken/channels/loldubr/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/loldubr/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/loldubr/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/loldubr\",\"features\":\"https://api.twitch.tv/kraken/channels/loldubr/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/loldubr/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/loldubr/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/loldubr/videos\"},\"teams\":[]}},{\"name\":\"live_user_vernnotice\",\"broadcaster\":\"xsplit\",\"_id\":8037767856,\"game\":\"League of Legends\",\"viewers\":499,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_vernnotice-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/vernnotice\"},\"channel\":{\"mature\":false,\"status\":\"Gold II from Bronze II\",\"display_name\":\"VernNotice\",\"game\":\"League of Legends\",\"_id\":29884841,\"name\":\"vernnotice\",\"created_at\":\"2012-04-19T02:44:48Z\",\"updated_at\":\"2013-12-30T01:23:00Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/vernnotice-profile_image-3af9dc54fa7a481d-300x300.jpeg\",\"banner\":null,\"video_banner\":null,\"background\":null,\"url\":\"http://www.twitch.tv/vernnotice\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/vernnotice\",\"follows\":\"https://api.twitch.tv/kraken/channels/vernnotice/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/vernnotice/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/vernnotice/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/vernnotice\",\"features\":\"https://api.twitch.tv/kraken/channels/vernnotice/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/vernnotice/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/vernnotice/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/vernnotice/videos\"},\"teams\":[]}},{\"name\":\"live_user_h4ckerv2\",\"broadcaster\":\"obs\",\"_id\":8038161904,\"game\":\"League of Legends\",\"viewers\":431,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_h4ckerv2-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/h4ckerv2\"},\"channel\":{\"mature\":false,\"status\":\"h4ckerv2 - Jungle - Espanish hue\",\"display_name\":\"H4ckerv2\",\"game\":\"League of Legends\",\"_id\":14227923,\"name\":\"h4ckerv2\",\"created_at\":\"2010-07-31T01:22:52Z\",\"updated_at\":\"2013-12-30T02:03:13Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/h4ckerv2-profile_image-9203aff916447ea7-300x300.jpeg\",\"banner\":null,\"video_banner\":null,\"background\":null,\"url\":\"http://www.twitch.tv/h4ckerv2\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/h4ckerv2\",\"follows\":\"https://api.twitch.tv/kraken/channels/h4ckerv2/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/h4ckerv2/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/h4ckerv2/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/h4ckerv2\",\"features\":\"https://api.twitch.tv/kraken/channels/h4ckerv2/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/h4ckerv2/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/h4ckerv2/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/h4ckerv2/videos\"},\"teams\":[{\"_id\":877,\"name\":\"streamsmexico\",\"info\":\"\\n\",\"display_name\":\"Streams de México\",\"created_at\":\"2013-11-21T21:17:47Z\",\"updated_at\":\"2013-11-26T10:44:05Z\",\"logo\":null,\"banner\":null,\"background\":null,\"_links\":{\"self\":\"https://api.twitch.tv/kraken/teams/streamsmexico\"}}]}},{\"name\":\"live_user_lmqibuypower\",\"broadcaster\":\"obs\",\"_id\":8037993008,\"game\":\"League of Legends\",\"viewers\":417,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_lmqibuypower-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/lmqibuypower\"},\"channel\":{\"mature\":false,\"status\":\"<<LMQ Tc iBUYPOWER>>NA Ranked 5s 6pm PST !(QQ 173471899)\",\"display_name\":\"LMQiBUYPOWER\",\"game\":\"League of Legends\",\"_id\":53428206,\"name\":\"lmqibuypower\",\"created_at\":\"2013-12-18T00:21:57Z\",\"updated_at\":\"2013-12-30T01:45:49Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/lmqibuypower-profile_image-fceeba8485b79df2-300x300.jpeg\",\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/lmqibuypower-channel_offline_image-85cc43acbccd1c06-640x360.jpeg\",\"background\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/lmqibuypower-background_image-dcd5f42eb44b0e95.jpeg\",\"url\":\"http://www.twitch.tv/lmqibuypower\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/lmqibuypower\",\"follows\":\"https://api.twitch.tv/kraken/channels/lmqibuypower/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/lmqibuypower/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/lmqibuypower/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/lmqibuypower\",\"features\":\"https://api.twitch.tv/kraken/channels/lmqibuypower/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/lmqibuypower/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/lmqibuypower/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/lmqibuypower/videos\"},\"teams\":[]}},{\"name\":\"live_user_gripex90\",\"broadcaster\":\"obs\",\"_id\":8034163472,\"game\":\"League of Legends\",\"viewers\":394,\"preview\":\"http://static-cdn.jtvnw.net/previews-ttv/live_user_gripex90-320x200.jpg\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/streams/gripex90\"},\"channel\":{\"mature\":null,\"status\":\"Best Lee Sin EU - Diamond 1 Jungle/top - Testing Elder Lizard rush and loving it!\",\"display_name\":\"Gripex90\",\"game\":\"League of Legends\",\"_id\":32947748,\"name\":\"gripex90\",\"created_at\":\"2012-08-11T18:17:59Z\",\"updated_at\":\"2013-12-29T20:00:42Z\",\"logo\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/gripex90-profile_image-66581cfc89720490-300x300.jpeg\",\"banner\":null,\"video_banner\":\"http://static-cdn.jtvnw.net/jtv_user_pictures/gripex90-channel_offline_image-69d3afe9068f9a96-640x360.jpeg\",\"background\":null,\"url\":\"http://www.twitch.tv/gripex90\",\"_links\":{\"self\":\"https://api.twitch.tv/kraken/channels/gripex90\",\"follows\":\"https://api.twitch.tv/kraken/channels/gripex90/follows\",\"commercial\":\"https://api.twitch.tv/kraken/channels/gripex90/commercial\",\"stream_key\":\"https://api.twitch.tv/kraken/channels/gripex90/stream_key\",\"chat\":\"https://api.twitch.tv/kraken/chat/gripex90\",\"features\":\"https://api.twitch.tv/kraken/channels/gripex90/features\",\"subscriptions\":\"https://api.twitch.tv/kraken/channels/gripex90/subscriptions\",\"editors\":\"https://api.twitch.tv/kraken/channels/gripex90/editors\",\"videos\":\"https://api.twitch.tv/kraken/channels/gripex90/videos\"},\"teams\":[]}}]}";

        public Twixel(string id, string secret)
        {
            authorizedScopes = new List<Scope>();
            clientID = id;
            clientSecret = secret;
        }

        public async Task<List<Game>> RetrieveTopGames(bool getNext)
        {
            Uri uri;

            if (!getNext)
            {
                uri = new Uri("https://api.twitch.tv/kraken/games/top");
            }
            else
            {
                if (nextGames != null)
                {
                    uri = nextGames.url;
                }
                else
                {
                    uri = new Uri("https://api.twitch.tv/kraken/games/top");
                }
            }

            string responseString = await GetWebData(uri);
            return LoadGames(JObject.Parse(responseString));
        }

        public async Task<List<Game>> RetrieveTopGames(int limit, bool hls)
        {
            Uri uri;

            if (limit <= 100)
            {
                if (!hls)
                {
                    uri = new Uri("https://api.twitch.tv/kraken/games/top?limit=" + limit.ToString());
                }
                else
                {
                    uri = new Uri("https://api.twitch.tv/kraken/games/top?limit=" + limit.ToString() + "&hls=true");
                }
            }
            else
            {
                if (!hls)
                {
                    uri = new Uri("https://api.twitch.tv/kraken/games/top?limit=100");
                }
                else
                {
                    uri = new Uri("https://api.twitch.tv/kraken/games/top?limit=100&hls=true");
                }
                errorString = "The max number of top games you can get at one time is 100";
            }

            string responseString = await GetWebData(uri);
            return LoadGames(JObject.Parse(responseString));
        }

        public async Task<Stream> RetrieveStream(string channelName)
        {
            Uri uri;
            uri = new Uri("https://api.twitch.tv/kraken/streams/" + channelName);

            string responseString = await GetWebData(uri);
            JObject stream = JObject.Parse(responseString);
            if (stream["stream"].ToString() != "")
            {
                return LoadStream((JObject)stream["stream"]);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Stream>> RetrieveStreams(bool getNext)
        {
            Uri uri;
            if (!getNext)
            {
                uri = new Uri("https://api.twitch.tv/kraken/streams");
            }
            else
            {
                if (nextStreams != null)
                {
                    uri = nextStreams.url;
                }
                else
                {
                    uri = new Uri("https://api.twitch.tv/kraken/streams");
                }
            }

            string responseString = await GetWebData(uri);
            return LoadStreams(JObject.Parse(responseString));
        }

        public async Task<List<Stream>> RetrieveStreams(string game)
        {
            Uri uri;
            uri = new Uri("https://api.twitch.tv/kraken/streams?game=" + game);
            string responseString = await GetWebData(uri);
            return LoadStreams(JObject.Parse(responseString));
        }

        public async Task<List<Stream>> RetrieveStreams(string game, List<string> channels, int limit, bool embeddable, bool hls)
        {
            Uri uri;
            string uriString = "https://api.twitch.tv/kraken/streams";

            if (game != "")
            {
                uriString = "https://api.twitch.tv/kraken/streams?game=" + game + "&channel=";
                for (int i = 0; i < channels.Count; i++)
                {
                    if (i != channels.Count - 1)
                    {
                        uriString += channels[i] + ",";
                    }
                    else
                    {
                        uriString += channels[i];
                    }
                }

                if (limit <= 100)
                {
                    uriString += "&limit=" + limit.ToString();
                }
                else
                {
                    uriString += "&limit=100";
                }

                if (embeddable)
                {
                    uriString += "&embeddable=true";
                }

                if (hls)
                {
                    uriString += "&hls=true";
                }
            }
            else
            {
                string seperator = "?";

                if (channels.Count > 0)
                {
                    uriString += "?channel=";
                    seperator = "&";
                    for (int i = 0; i < channels.Count; i++)
                    {
                        if (i != channels.Count - 1)
                        {
                            uriString += channels[i] + ",";
                        }
                        else
                        {
                            uriString += channels[i];
                        }
                    }
                }

                if (limit <= 100)
                {
                    uriString += seperator + "limit=" + limit.ToString();
                }
                else
                {
                    uriString += seperator + "limit=100";
                }
                seperator = "&";

                if (embeddable)
                {
                    uriString += "&embeddable=true";
                }

                if (hls)
                {
                    uriString += "&hls=true";
                }
            }

            uri = new Uri(uriString);
            string responseString = await GetWebData(uri);
            return LoadStreams(JObject.Parse(responseString));
        }

        public async Task<List<FeaturedStream>> RetrieveFeaturedStreams()
        {
            Uri uri;
            uri = new Uri("https://api.twitch.tv/kraken/streams/featured");
            string responseString = await GetWebData(uri);
            return LoadFeaturedStreams(JObject.Parse(responseString));
        }

        public async Task<List<FeaturedStream>> RetrieveFeaturedStreams(int limit, bool hls)
        {
            Uri uri;
            if (!hls)
            {
                uri = new Uri("https://api.twitch.tv/kraken/streams/featured?limit=" + limit.ToString());
            }
            else
            {
                uri = new Uri("https://api.twitch.tv/kraken/streams/featured?limit=" + limit.ToString() + "&hls=true");
            }
            string responseString = await GetWebData(uri);
            return LoadFeaturedStreams(JObject.Parse(responseString));
        }

        public async Task RetrieveStreamsSummary()
        {
            Uri uri;
            uri = new Uri("https://api.twitch.tv/kraken/streams/summary");
            string responseString = await GetWebData(uri);
            JObject summary = JObject.Parse(responseString);
            summaryViewers = (int)summary["viewers"];
            summaryChannels = (int)summary["channels"];
        }

        public async Task<Uri> Login(List<Scope> scopes)
        {
            if (scopes.Count > 0)
            {
                List<Scope> cleanScopes = new List<Scope>();

                for (int i = 0; i < scopes.Count; i++)
                {
                    if (!cleanScopes.Contains(scopes[i]))
                    {
                        cleanScopes.Add(scopes[i]);
                    }
                    else
                    {
                        scopes.RemoveAt(i);
                        i--;
                    }
                }
                Uri uri;
                uri = new Uri("https://api.twitch.tv/kraken/oauth2/authorize" +
                "?response_type=token" +
                "&client_id=" + clientID +
                "&redirect_uri=http://golf1052.com" +
                "&scope=");
                string originalString = uri.OriginalString;
                foreach (Scope scope in scopes)
                {
                    originalString += ScopeToString(scope) + " ";
                }
                uri = new Uri(originalString);
                return uri;
            }
            else
            {
                errorString = "You must have at least 1 scope";
                return null;
            }
        }

        List<Game> LoadGames(JObject o)
        {
            List<Game> games = new List<Game>();
            nextGames = new WebUrl((string)o["_links"]["next"]);
            maxGames = (int)o["_total"];

            foreach (JObject obj in (JArray)o["top"])
            {
                games.Add(new Game((string)obj["game"]["name"],
                    (JObject)obj["game"]["box"],
                    (JObject)obj["game"]["logo"],
                    (long?)obj["game"]["_id"],
                    (long?)obj["game"]["giantbomb_id"],
                    (int?)obj["viewers"],
                    (int?)obj["channels"]));
            }

            return games;
        }

        List<Stream> LoadStreams(JObject o)
        {
            List<Stream> streams = new List<Stream>();
            nextStreams = new WebUrl((string)o["_links"]["next"]);

            foreach (JObject obj in (JArray)o["streams"])
            {
                streams.Add(LoadStream(obj));
            }

            return streams;
        }

        Stream LoadStream(JObject o)
        {
            return new Stream((string)o["broadcaster"],
                    (long)o["_id"],
                    (string)o["preview"],
                    (string)o["game"],
                    (JObject)o["channel"],
                    (string)o["name"],
                    (int)o["viewers"]);
        }

        List<FeaturedStream> LoadFeaturedStreams(JObject o)
        {
            List<FeaturedStream> streams = new List<FeaturedStream>();
            nextStreams = new WebUrl((string)o["_links"]["next"]);

            foreach (JObject obj in (JArray)o["featured"])
            {
                streams.Add(new FeaturedStream((string)obj["image"], (string)obj["text"], (JObject)obj["stream"]));
            }

            return streams;
        }

        public static async Task<string> GetWebData(Uri uri)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v2+json");
            client.DefaultRequestHeaders.Add("Client-ID", clientID);
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // 200 - OK
                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // 400 - Bad request
                return "400";
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // 401 - Unauthoriezed
                return "401";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // 404 - Summoner not found
                return "404";
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                // 500 - Internal server error
                return "500";
            }
            else
            {
                return "Unknown status code";
            }
        }

        public static string ScopeToString(Scope scope)
        {
            if (scope == Scope.UserRead)
            {
                return "user_read";
            }
            else if (scope == Scope.UserBlocksEdit)
            {
                return "user_blocks_edit";
            }
            else if (scope == Scope.UserBlocksRead)
            {
                return "user_blocks_read";
            }
            else if (scope == Scope.UserFollowsEdit)
            {
                return "user_follows_edit";
            }
            else if (scope == Scope.ChannelRead)
            {
                return "channel_read";
            }
            else if (scope == Scope.ChannelEditor)
            {
                return "channel_editor";
            }
            else if (scope == Scope.ChannelCommercial)
            {
                return "channel_commercial";
            }
            else if (scope == Scope.ChannelStream)
            {
                return "channel_stream";
            }
            else if (scope == Scope.ChannelSubscriptions)
            {
                return "channel_subscriptions";
            }
            else if (scope == Scope.UserSubcriptions)
            {
                return "user_subscriptions";
            }
            else if (scope == Scope.ChannelCheckSubscription)
            {
                return "channel_check_subscriptions";
            }
            else if (scope == Scope.ChatLogin)
            {
                return "chat_login";
            }
            else
            {
                return "none";
            }
        }

        public static Scope StringToScope(string scope)
        {
            if (scope == "user_read")
            {
                return Scope.UserRead;
            }
            else if (scope == "user_blocks_edit")
            {
                return Scope.UserBlocksEdit;
            }
            else if (scope == "user_blocks_read")
            {
                return Scope.UserBlocksRead;
            }
            else if (scope == "user_follows_edit")
            {
                return Scope.UserFollowsEdit;
            }
            else if (scope == "channel_read")
            {
                return Scope.ChannelRead;
            }
            else if (scope == "channel_editor")
            {
                return Scope.ChannelEditor;
            }
            else if (scope == "channel_commercial")
            {
                return Scope.ChannelCommercial;
            }
            else if (scope == "channel_stream")
            {
                return Scope.ChannelStream;
            }
            else if (scope == "channel_subscriptions")
            {
                return Scope.ChannelSubscriptions;
            }
            else if (scope == "user_subscriptions")
            {
                return Scope.UserSubcriptions;
            }
            else if (scope == "channel_check_subscriptions")
            {
                return Scope.ChannelCheckSubscription;
            }
            else if (scope == "chat_login")
            {
                return Scope.ChatLogin;
            }
            else
            {
                return Scope.None;
            }
        }
    }
}
