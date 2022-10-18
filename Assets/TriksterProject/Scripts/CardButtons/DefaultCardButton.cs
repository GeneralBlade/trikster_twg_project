using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace TriksterProject
{
    public class DefaultCardButton : MonoBehaviour, IPointerClickHandler
    {

        public GameObject messageCard;
        public GameObject messageText;
        public float timeDeactiveMessageCard = 15f;
        
        // public string messageText = "";
        public GameObject titleCardPanel;
        public GameObject titleCardText;

        

        public bool isNoneCard = false;

        public ColodCards colodCards;

        public enum MastEnum{None, House, Person, Action, Event, Item, Mask, Quest};
        public MastEnum mast;
        public string idCard;
        public string nameCard;
        public string message = "";
        public bool isUnical = false;
        public bool isUndestroy = false;

        public GameObject[] onCards;
        public GameObject[] returnCards;
        public bool[] isOnCardTransform;


        /*
        public Sprite FaceCardSprite;
        public Sprite BackCardSprite;
        public bool isNoneCard = true;
        public GameObject ChildCard;
        */

        /* 
        public bool isPlace = false;
        public bool isPerson = true;
        public bool isAction = false;

        public int stealth = 0;
        public int attack = 0;
        public int hack = 0;
        public int suspicion = 0;
        public int panic = 0;
        public int security = 0;

        public int sizePlace = 1; 
        */

        /* public Texture2D cursorTexture;
        protected CursorMode cursorMode = CursorMode.ForceSoftware; // CursorMode.Auto;
        protected Vector2 hotSpot = Vector2.zero; */

        protected GameObject handCursor;
        protected GameObject cardsCanvas;

        // Start is called before the first frame update
        void Start()
        {
            GameObject[] handCursors;
            handCursors = GameObject.FindGameObjectsWithTag("HandCursor");

            if (handCursors.Length > 0) { handCursor = handCursors[0]; }

            GameObject[] cardsCanvases;
            cardsCanvases = GameObject.FindGameObjectsWithTag("CardsCanvas");

            if (cardsCanvases.Length > 0) { cardsCanvas = cardsCanvases[0]; }

            if (isNoneCard || colodCards)
            {
                titleCardPanel.SetActive(false);
            }
            else
            {

                titleCardText.GetComponent<Text>().text = nameCard;
                
                switch(mast)
                {
                    case MastEnum.None: 
                        break;
                    case MastEnum.House:
                        titleCardPanel.GetComponent<Image>().color = Color.yellow;
                        break;
                    case MastEnum.Person:
                        titleCardPanel.GetComponent<Image>().color = Color.green;
                        break;
                    case MastEnum.Action:
                        titleCardPanel.GetComponent<Image>().color = Color.magenta;
                        break;
                    case MastEnum.Event:
                        titleCardPanel.GetComponent<Image>().color = Color.red;
                        break;
                    case MastEnum.Item:
                        titleCardPanel.GetComponent<Image>().color = new Color(1f, 0.6f, 0f, 1f);
                        break;
                    case MastEnum.Mask:
                        titleCardPanel.GetComponent<Image>().color = new Color(0.9f, 0.3f, 0.8f, 1f);
                        break;
                    case MastEnum.Quest:
                        titleCardPanel.GetComponent<Image>().color = Color.blue;
                        break;
                    default: 
                        break;
                }
                titleCardPanel.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            //Use this to tell when the user right-clicks on the Button
            if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
                // Debug.Log(name + " Game Object Right Clicked!");
                // messageCard.GetComponent<Text>().text = messageText;
                messageText.GetComponent<Text>().text = message;
                messageCard.SetActive(true);
                Invoke("DeactiveMessageCard", timeDeactiveMessageCard);
            }

            // Use this to tell when the user left-clicks on the Button
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log(name + " Game Object Left Clicked!");

                /* cursorTexture = GetComponent<Image>().mainTexture;
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); */


                if (colodCards)
                {
                    /* if (handCursor.GetComponent<HandCursor>().cardAtHand)
                    {
                        GameObject cardAtHand = handCursor.GetComponent<HandCursor>().cardAtHand;

                        GameObject cardOnCard = Instantiate(cardAtHand);
                        cardOnCard.transform.SetParent(cardsCanvas.transform, false);
                        cardOnCard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        cardOnCard.SetActive(true);

                        Destroy(handCursor.GetComponent<HandCursor>().cardAtHand);
                        handCursor.GetComponent<HandCursor>().cardAtHand = null;

                    } */
                    if (!handCursor.GetComponent<HandCursor>().cardAtHand)
                    {
                        // GameObject cardAtHand = handCursor.GetComponent<HandCursor>().cardAtHand;
                        handCursor.GetComponent<HandCursor>().cardAtHand = Instantiate(colodCards.GetCard());
                        // Destroy(transform.gameObject);
                    }

                } else
                {
                    switch (isNoneCard)
                    {
                        case true:
                            if (handCursor.GetComponent<HandCursor>().cardAtHand)
                            {
                                GameObject cardAtHand = handCursor.GetComponent<HandCursor>().cardAtHand;

                                GameObject cardOnCard = Instantiate(cardAtHand);
                                cardOnCard.transform.SetParent(cardsCanvas.transform, false);
                                cardOnCard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                                cardOnCard.SetActive(true);

                                Destroy(handCursor.GetComponent<HandCursor>().cardAtHand);
                                handCursor.GetComponent<HandCursor>().cardAtHand = null;

                            }
                            else
                            {
                                break;
                            }

                            break;

                        case false:
                            if (handCursor.GetComponent<HandCursor>().cardAtHand)
                            {
                                // break;

                                GameObject cardAtHand = handCursor.GetComponent<HandCursor>().cardAtHand;

                                Debug.Log("for stepCard start");
                                for (int stepOnCard = 0; stepOnCard < onCards.Length; stepOnCard++)
                                {
                                    Debug.Log("for stepCard iteration");
                                    Debug.Log("if stepCard start");
                                    // if (cardAtHand == onCards[stepOnCard])
                                    
                                    Debug.Log(
                                            "cardAtHand = " + cardAtHand + 
                                            " stepOnCard = " + stepOnCard + 
                                            " returnCards[stepOnCard] = " + returnCards[stepOnCard] +
                                            " onCards[stepOnCard] = " + onCards[stepOnCard]
                                        );

                                    if (cardAtHand.GetComponent<DefaultCardButton>().nameCard == onCards[stepOnCard].GetComponent<DefaultCardButton>().nameCard)
                                    {
                                        Debug.Log("if stepCard iteration");

                                        bool boolIsOnCardTransform = false;
                                        /* if (isOnCardTransform)
                                        { */
                                        if ((isOnCardTransform.Length - 1) >= stepOnCard)
                                        {
                                            boolIsOnCardTransform = isOnCardTransform[stepOnCard];
                                        }
                                        // }

                                        TransformCard(returnCards[stepOnCard], boolIsOnCardTransform);
                                    }
                                    Debug.Log("if stepCard end");
                                }
                                Debug.Log("for stepCard end");
                            }
                            else
                            {
                                // GameObject cardAtHand = handCursor.GetComponent<HandCursor>().cardAtHand;
                                handCursor.GetComponent<HandCursor>().cardAtHand = Instantiate(transform.gameObject);
                                Destroy(transform.gameObject);
                            }

                            break;

                        default:
                            break;
                    }
                }
                
            } 
        }

        protected void TransformCard(GameObject finishCard, bool isOnCardTransform = false)
        {
            TransformCard(
                    transform.gameObject,
                    handCursor.GetComponent<HandCursor>().cardAtHand,
                    finishCard, 
                    isOnCardTransform, 
                    isNoneCard 
                );
        }

        protected void TransformCard(
                GameObject thisGameObject,
                GameObject cardAtHand,
                GameObject finishCard,
                bool isOnCardTransform = false,
                bool isNoneCard = false
            )
        {
            // GameObject cardAtHand = handCursor.GetComponent<HandCursor>().cardAtHand;
            if (!isOnCardTransform)
            {
                GameObject cardOnCard = new GameObject();

                if (!isNoneCard)
                {
                    cardOnCard = Instantiate(finishCard);
                }
                else
                {
                    cardOnCard = Instantiate(cardAtHand);
                }

                cardOnCard.transform.SetParent(cardsCanvas.transform, false);
                cardOnCard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                cardOnCard.SetActive(true);

                Destroy(handCursor.GetComponent<HandCursor>().cardAtHand);
                handCursor.GetComponent<HandCursor>().cardAtHand = null;

                if (!isNoneCard)
                {
                    Destroy(transform.gameObject);
                }
            }
            else
            {
                /* handCursor.GetComponent<HandCursor>().cardAtHand = Instantiate(finishCard);
                Destroy(transform.gameObject); */

                handCursor.GetComponent<HandCursor>().SetCardAtHand(Instantiate(finishCard));

            }
            

        }

        protected void DeactiveMessageCard()
        {
            messageCard.SetActive(false);
        }
    }
}
