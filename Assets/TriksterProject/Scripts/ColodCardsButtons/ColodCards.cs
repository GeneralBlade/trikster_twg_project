using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TriksterProject
{
    public class ColodCards : MonoBehaviour
    {

        public GameObject[] cardsArray;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject GetCard()
        {
            GetCard:
                int indexArray = Random.Range(0, cardsArray.Length);
                GameObject card = cardsArray[indexArray];
                if (!card) { goto GetCard; }
                if (card.GetComponent<DefaultCardButton>().isUnical)
                {
                    cardsArray[indexArray] = null;
                }

            return card;
        }
    }
}

