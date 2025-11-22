using UnityEngine;

public class Character : MonoBehaviour
{
    private ObjectFader _fader;

    private void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                    return;

                if (hit.collider.gameObject == player)
                {
                    if (_fader != null)
                    {
                        _fader.isFading = false;
                    }
                }
                else
                {
                    _fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (_fader != null)
                    {
                        _fader.isFading = true;
                    }
                }
            }

        }
    }

}
