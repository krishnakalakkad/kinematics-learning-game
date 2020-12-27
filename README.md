# Kinematics Learning Game

## Project Overview
This is a unity power web browser game intended to teach high schoolers and middle schoolers some of the basics of Kinematics. It is part of a STEM workshop called "Seeds in Stem"
which runs for students in the Santa Maria Unified School District every winter. This project is for the Winter 2021 workshop.
<img src="/image/link/to/file">

## Motivations and Goals
What was the context of building this project? What were you looking to learn? Was this for a class?

This was part of a project based learning class. I was assigned to a team, and our goal was to design and schedule one third of the workshop. While brainstorming ideas for the workshop,
I proposed the idea of creating a video game to teach basic kinematics through the narrative of a space ranger altering the net force acting on them by using a jetpack.

## Challenges

### Unity and C# learning curve

The main challenge was learning C# and Unity. I didn't know any C# or Unity at the time, so I felt overwhelmed. I was also on an Agile project management schedule, so I had two weeks to 
display results.

**My Approach**

What did you do to tackle this challenge? Walk us through the steps yo

I used a six step process:

1. Identify what I need to do next for the project.
2. Search on forums on how to do this.
3. Attempt to do it and fail.
4. Search forums again to debug.
5. Debug.
6. Repeat.

This method is how I wrote my function that lets the player look around:

```
void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
```

This method also allowed me to incorporate raycasts so that the player could click objects in the distance to find how to far away they are:

```
void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetMouseButtonDown(0)){

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)){
                if (hit.transform != null){
                    SetHeightReference(hit.transform);
                }
            }
        }
    }
```

**Result**

Through this methodology, I learned a very specialized skillset in Unity and C# that can be applied to first person games. This skillset includes the use of GameObject, raycasting, usage of
quaternions, transforms, and Vector3 objects. I also got to apply my knowledge in kinematics and see how it would fit in the frame of game design. This can be seen in my `ChangeGravity()` function.

```
public void ChangeGravity(){
        jetString = inputField.GetComponent<Text>().text;
        jetForce = float.Parse(jetString, CultureInfo.InvariantCulture.NumberFormat);
        netForce = jetForce;
        jumpHeight = (velocity.y * velocity.y) / (-2f * -9.81f);
    }
```



## How to use this program
**OPTIONAL** 
Include how to download and install dependancies. Here would also be a good place to define anything that is currently WIP. 


