using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Areas.Common.Runtime;
using GamePlay.Player.Entity.Components.Definition;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Abstract;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Services.Common.Scope;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.AssetsFlow.Runtime.Abstract;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Network.Instantiators.Runtime;
using Global.Services.Profiles.Storage;
using Ragon.Client;
using UniRx;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public class PlayerFactory : MonoBehaviour, IPlayerFactory
    {
        [Inject]
        private void Construct(
            IAssetInstantiatorFactory instantiatorFactory,
            INetworkInstantiator networkInstantiator,
            LevelScope scope,
            IPlayerEntityPresenter entityPresenter,
            IProfileStorageProvider profileStorageProvider,
            PlayerFactoryConfigAsset configAsset,
            PlayerFactoryLogger logger,
            IReputation reputation)
        {
            _reputation = reputation;
            _instantiatorFactory = instantiatorFactory;
            _entityPresenter = entityPresenter;
            _profileStorageProvider = profileStorageProvider;
            _networkInstantiator = networkInstantiator;
            _logger = logger;
            _scope = scope;
            _configAsset = configAsset;
        }

        private PlayerFactoryConfigAsset _configAsset;

        private LevelScope _scope;
        private PlayerFactoryLogger _logger;

        private INetworkInstantiator _networkInstantiator;
        private IProfileStorageProvider _profileStorageProvider;
        private IPlayerEntityPresenter _entityPresenter;
        private IAssetInstantiatorFactory _instantiatorFactory;
        private IReputation _reputation;

        public async UniTask<IPlayerRoot> Create(Vector2 position, ShipType type)
        {
            var payload = new PlayerPayload(_profileStorageProvider.UserName, type, _reputation.Faction);

            var networkObject = await _networkInstantiator.Instantiate<PlayerNetworkRoot, PlayerPayload>(
                _configAsset.NetworkPrefab,
                position,
                payload);

            var prefab = _configAsset.GetShip(type);
            var instantiator = _instantiatorFactory.Create<GameObject>(prefab);
            var playerObject = await instantiator.InstantiateAsync(Vector2.zero);

            var playerTransform = playerObject.transform;
            var networkTransform = networkObject.transform;
            var entity = networkTransform.GetComponent<RagonEntity>();

            playerTransform.parent = networkTransform;
            playerTransform.localPosition = Vector3.zero;

            _logger.OnInstantiated(position);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            var resources = playerTransform.GetComponent<IAreaInteractor>().Resources;
            _entityPresenter.AssignPlayer(entity, networkTransform, resources);

            Msg.Publish(new PlayerSpawnedEvent());

            return root;
        }

        public async UniTask<IPlayerRoot> CreateBot(Vector2 position, ShipType type)
        {
            var payload = new PlayerPayload(GetName(), type, _reputation.Faction);

            var networkObject = await _networkInstantiator.Instantiate<PlayerNetworkRoot, PlayerPayload>(
                _configAsset.NetworkPrefab,
                position,
                payload);

            var prefab = _configAsset.GetBotShip(type);
            var instantiator = _instantiatorFactory.Create<GameObject>(prefab);
            var playerObject = await instantiator.InstantiateAsync(Vector2.zero);

            var playerTransform = playerObject.transform;
            var networkTransform = networkObject.transform;
            var entity = networkTransform.GetComponent<RagonEntity>();

            playerTransform.parent = networkTransform;
            playerTransform.localPosition = Vector3.zero;

            _logger.OnInstantiated(position);

            var bootstrapper = playerObject.GetComponent<IPlayerBootstrapper>();

            await bootstrapper.Bootstrap(_scope);

            var root = playerObject.GetComponent<IPlayerRoot>();

            return root;
        }

        private string GetName()
        {
            return _names[Random.Range(0, _names.Count)];
        }

        private List<string> _names = new()
        {
            "nging_with_my_gnomies          ",
            "osier-daddy                    ",
            "st_and_the_curious             ",
            "eragestudent                   ",

            "dKarma                         ",

            "ogle_was_my_idea               ",

            "te.as.ducks                    ",


            "sanova                         ",

            "eal_name_hidden                ",

            "airyPoppins                    ",

            "edora_the_explorer             ",

            "P_rah                          ",

            "ellowSnowman                   ",

            "ash                            ",
            "sername_copied                 ",

            "hos_ur_buddha                  ",

            "nfinished_sentenc              ",

            "llGoodNamesRGone               ",

            "omething                       ",

            "e_for_president                ",

            "infoilhat                      ",

            "prahwindfury                   ",

            "nonymouse                      ",


            "eartTicker                     ",

            "ESIMFUNNY                      ",

            "enAfleckIsAnOkActor            ",

            "agicschoolbusdropout           ",

            "verybody                       ",

            "egina_phalange                 ",

            "awneeGoddess                   ",

            "luralizes_everythings          ",

            "hickenriceandbeans             ",

            "YELLALOT                       ",

            "eyyou                          ",

            "augh_till_u_pee                ",

            "Distraction                    ",

            "razy_cat_lady                  ",

            "anana_hammock                  ",

            "hegodfatherpart4               ",

            "nfriendme                      ",

            "abydoodles                     ",

            "luffycookie                    ",

            "uh-buh-bacon                   ",

            "shley_said_what                ",

            "actoseTheIntolerant            ",

            "anEatsPants                    ",

            "wentyfourhourpharmacy          ",

            "pplebottomjeans                ",

            "abushka                        ",

            "oastedbagelwithcreamcheese     ",

            "aeconandeggz                   ",

            "artinLutherKing                ",

            "oolshirtbra                    ",

            "entuckycriedfricken            ",

            "EVERANDTOAST                   ",

            "im_chi                         ",

            "drinkchocolatemilk             ",

            "aintBroseph                    ",

            "hin_chillin                    ",

            "hostfacegangsta                ",

            "igfootisreal                   ",

            "antas_number1_elf              ",

            "hehornoftheunicorn             ",

            "Need2p                         ",

            "bductedbyaliens                ",

            "ctuallynotchrishemsworth       ",

            "achocheesefries                ",

            "ust-a-harmless-potato          ",

            "rostedCupcake                  ",

            "vocadorable                    ",

            "ash                            ",
            "atBatman                       ",

            "uailandduckeggs                ",

            "aniniHead                      ",

            "andymooressingingvoice         ",

            "atsordogs                      ",

            "artnRoses                      ",

            "edMonkeyButt                   ",

            "reddyMercurysCat               ",

            "asterCheif                     ",

            "reeHugz                        ",

            "ma.robot                       ",

            "ctuallythedog                  ",

            "otthetigerking                 ",

            "ixie_dust                      ",

            "hopSuey                        ",


            "urkey_sandwich                 ",

            ".Juice                         ",

            "hris_P_Bacon                   ",

            "tDansLegs                      ",

            "okiesrPpl2                     ",

            "ogwartsfailure                 ",

            "ourtesyFlush                   ",

            "omsSpaghetti                   ",

            "pongebobspineapple             ",

            "garythesnail                   ",

            "nothisispatrick                ",

            "CountSwagula                   ",

            "SweetP                         ",

            "PNUT                           ",

            "Snax                           ",

            "Nuggetz                        ",

            "colonel_mustards_rope          ",

            "baby_bugga_boo                 ",

            "joancrawfordfanclub            ",

            "fartoolong                     ",

            "loliateyourcat                 ",

            "rawr_means_iloveyou            ",

            "ihavethingstodo.jpg            ",

            "heresWonderwall                ",

            "UFO_believer                   ",

            "ihazquestion                   ",

            "SuperMagnificentExtreme        ",

            "It’s_A _Political_ Statement   ",

            "TheAverageForumUser            ",

            "just_a_teen                    ",

            "OmnipotentBeing                ",

            "GawdOfROFLS                    ",

            "loveandpoprockz                ",

            "ll to keep reading)            ",
            "ed Stories                     ",
            "last-names                     ",

            "Bread Pitt                     ",

            "ash                            ",
            "rejectedbachelorcontestant     ",

            "Schmoople                      ",

            "LOWERCASE GUY                  ",

            "Unnecessary                    ",

            "joan_of_arks_angel             ",

            "InstaPrincess                  ",

            "DroolingOnU                    ",

            "Couldnt_Find_Good_Name         ",

            "AngelWonderland                ",

            "Born-confused                  ",

            "SargentSaltNPepa               ",

            "DosentAnyoneCare               ",

            "quaratineinthesejeans          ",

            "thanoslefthand                 ",

            "ironmansnap                    ",

            "chalametbmybae                 ",

            "peterparkerspuberty            ",

            "severusvape                    ",

            "theotherharrypotter            ",

            "GrangerDanger                  ",

            "BlueIvysAssistant              ",

            "Ariana_Grandes_Ponytail        ",

            "HotButteryPopcorn              ",

            "MelonSmasher                   ",

            "morgan_freeman_but_not         ",

            "potatoxchipz                   ",

            "FoxtrotTangoLove               ",

            "ElfishPresley                  ",

            "WustacheMax                    ",

            "JuliusSeizure                  ",

            "HeyYouNotYouYou                ",

            "OneTonSoup                     ",

            "HoneyLemon                     ",

            "LoveMeKnot                     ",

            "Bud Lightyear                  ",

            "takenbyWine                    ",

            "taking0ver                     ",

            "Unic0rns                       ",

            "in_jail_out_soon               ",

            "hotgirlbummer                  ",

            "behind_you                     ",

            "itchy_and_scratchy             ",

            "not_james_bond                 ",

            "a_collection_of_cells          ",

            "CowabungaDude                  ",

            "TeaBaggins                     ",

            "bill_nye_the_russian_spy       ",

            "intelligent_zombie             ",

            "imma_rage_quit                 ",

            "kiss-my-axe                    ",

            "ash                            ",
            "king_0f_dairy_queen            ",

            "desperate_enuf                 ",

            "AirisWindy                     ",

            "cheeseinabag                   ",

            "MakunaHatata                   ",

            "ed: 200+ Funny Jokes           ",

            "Username Ideas                 ",
            "rambo_was_real                 ",

            "churros4eva                    ",

            "namenotimportant               ",

            "i_boop_ur_nose                 ",

            "image_not_uploaded             ",

            "suck_my_popsicle               ",

            "sofa_king_cool                 ",

            "RootinTootinPutin              ",

            "blousesandhouses               ",

            "iblamejordan                   ",

            "manic_pixie_meme_ girl         ",

            "Technophyle                    ",

            "Cuddly-Wuddly                  ",

            "JesusoChristo                  ",

            "peap0ds                        ",

            "whats_ur_sign                  ",

            "TheMilkyWeigh                  ",

            "BabyBluez                      ",

            "BarbieBreath                   ",

            "MangoGoGo                      ",

            "DirtBag                        ",

            "FurReal                        ",

            "ScoobyCute                     ",

            "YouIntradouchingMyshelf        ",

            "IwasReloading                  ",

            "WellEndowedPenguin             ",

            "TheAfterLife                   ",

            "PuppiesnKittens                ",

            "WakeAwake                      ",

            "Coronacosmo                    ",

            "wherearetheavocados            ",

            "ijustwanttobeme                ",

            "TheKidsCallMeBoss              ",

            "SewerSquirrel                  ",

            "because.i.like.to.like         ",

            "notmuchtoit                    ",

            "friedchocolate                 ",

            "DonWorryItsGonBK               ",

            "Early_Morning_Coffee           ",

            "drunkbetch                     ",

            "strawberry_pineapple           ",

            "MissPiggysDimples              ",

            "chickenbaconranchpizza         ",

            "cereal_killer                  ",

            "ash                            ",
            "khaleesisfourthdragon          ",

            "darth.daenerys                 ",

            "LeslieKnopesBinder             ",

            "BettyBoopsBoop                 ",

            "Freddie_Not_The_Fish           ",

            "Billys_Mullet                  ",

            "Calzone_Zone                   ",

            "ChickyChickyParmParm           ",

            "Adobo_Ahai                     ",

            "theoldRazzleDazzle             ",

            "Not-Insync                     ",

            "Toiletpaperman                 ",

            "Reese WitherFork               ",

            "LizzosFlute                    ",

            "Macauliflower Culkin           ",

            "Llama del Rey                  ",

            "Hot Name Here                  ",

            "Carmelpoptart                  ",

            "ed: Best Fantasy Football Names",

            "notfunnyatall                  ",

            "Mangonificent                  ",

            "toastcrunch                    ",

            "fizzysodas                     ",

            "kokonuts                       ",

            "cherry-picked                  ",
        };
    }
}