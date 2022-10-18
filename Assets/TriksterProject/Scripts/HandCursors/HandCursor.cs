using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TriksterProject
{
    public class HandCursor : MonoBehaviour
    {

        public Canvas canvasCard;
        public GameObject cardAtHand;
        public float scaleCard = 1.2f;

        protected GameObject spriteCardAtHand;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /* if (Input.GetMouseButtonDown(0))
            {
                spriteCardAtHand.SetActive(false);
            }

            if (Input.GetMouseButtonUp(0))
            {
                spriteCardAtHand.SetActive(true);
            } */
        }

        void FixedUpdate()
        {
            if (cardAtHand)
            {
                _UpdateSpriteCardAtHand();
            }
            else
            {
                if (spriteCardAtHand)
                {
                    Destroy(spriteCardAtHand);

                }
            }
        }

        public void SetCardAtHand(GameObject cardAtHand)
        {
            Destroy(this.cardAtHand);
            Destroy(this.spriteCardAtHand);
            this.cardAtHand = cardAtHand;
            _UpdateSpriteCardAtHand();
        }

        protected void _UpdateSpriteCardAtHand()
        {
            if (spriteCardAtHand)
            {
                _GameObjactToPosCursor(spriteCardAtHand);

            }
            else
            {
                _SetSpriteCardAtHand();
            }
        }

        protected void _SetSpriteCardAtHand()
        {

            spriteCardAtHand = Instantiate(cardAtHand);
            spriteCardAtHand.transform.SetParent(canvasCard.transform, false);
            spriteCardAtHand.transform.localScale = new Vector3(
                spriteCardAtHand.transform.localScale.x * scaleCard,
                spriteCardAtHand.transform.localScale.y * scaleCard,
                spriteCardAtHand.transform.localScale.z
            );



            Destroy(spriteCardAtHand.GetComponent<Button>());
            spriteCardAtHand.GetComponent<Image>().raycastTarget = false;
            spriteCardAtHand.GetComponent<DefaultCardButton>().messageCard.SetActive(false);
            cardAtHand.GetComponent<DefaultCardButton>().messageCard.SetActive(false);


            _GameObjactToPosCursor(spriteCardAtHand);
        }

        protected void _GameObjactToPosCursor(GameObject movingObj)
        {
            movingObj.transform.position = new Vector3(Input.mousePosition.x + 20f, Input.mousePosition.y + 2f, Input.mousePosition.z);
            movingObj.transform.localPosition = new Vector3(movingObj.transform.localPosition.x + 30f, movingObj.transform.localPosition.y + 7.5f, movingObj.transform.localPosition.z);
            /* movingObj.GetComponent<RectTransform>().localPosition = new Vector3(
                movingObj.GetComponent<RectTransform>().localPosition.x,
                movingObj.GetComponent<RectTransform>().localPosition.y,
                -100f
            ); */

        }
    }
}

