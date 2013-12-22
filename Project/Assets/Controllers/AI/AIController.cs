/*
 * How this will work...
 * When a Ship has to move, GameboardController will call startMove
 * startMove will do the things needed to compute a move.
 * Then it will call endMove
 * 
 * A team will own the AIController.
 */
using UnityEngine;
using System.Collections.Generic;

public class AIController {

    protected GameboardController myController;
    protected ShipController ship;
    protected ShipController target;
    protected int aiState = 0;
    protected double wait = 0;

    public virtual void startMove(GameboardController cntrl, ShipController myShip)
    {
        myController = cntrl;
        ship = myShip;
        aiState = 0; wait = 0;
        // Calculation of next move here.
        
        // Assess targets:
        List<ShipController> targets = new List<ShipController>(cntrl.shipList);
        target = null;
        int targetDistance = 1000000;
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].myShip.getTeam() != myShip.myShip.getTeam() && targets[i].myShip != null && targets[i].myShip.getHP() > 0)
            {
                if (myShip.myShip.getPosition().getHexDistance(targets[i].myShip.getPosition()) < targetDistance)
                {
                    target = targets[i];
                    targetDistance = myShip.myShip.getPosition().getHexDistance(targets[i].myShip.getPosition());
                }
            }
        }

        Debug.Log("Start Moving");
        // Find a path to the target.
        if (target == null) return;
        string path = getHexPath(myShip.myShip.getPosition(), target.myShip.getPosition());
        Debug.Log(path);
        myShip.move(path);
        aiState = 1;
    }

    public virtual void update() 
    {
        if (ship == null || myController == null) return;
        if (aiState == 1)
        {
            if (ship.isDoneMoving())
            {
                Debug.Log("Done Moving");
                aiState = 2;
            }
        }
        if (aiState == 2)
        {
            // Fire at the target.
            int distance = ship.myShip.getPosition().getHexDistance(target.myShip.getPosition());
            wait = 1;
            if (distance <= 3)
            {
                Debug.Log("Shooting");
                ship.fire(target);
                wait += 2;
            }
            aiState = 3;
        }
        if (aiState == 3)
        {
            wait -= Time.deltaTime;
            if (wait <= 0)
            {
                wait = 0;
                aiState = 4;
            }
        }
        if (aiState == 4)
        {
            Debug.Log("Done");
            endMove();
        }
    }

    public virtual void endMove()
    {
        aiState = 0;
        wait = 0;
        myController.onMoveFinish();
    }

    private string findHexPath(Hex start, Hex end)
    {
        if (start == end) return "0";

        Dictionary<Hex, string> toVisit = new Dictionary<Hex, string>();
        Dictionary<Hex, string> visited = new Dictionary<Hex, string>();
        toVisit[start] = "";

        while (!visited.ContainsKey(end) && toVisit.Count > 0)
        {
            Hex h = (new List<Hex>(toVisit.Keys))[0];
            if (visited.ContainsKey(h)) toVisit.Remove(h);
            else
            {
                string pathToMe = toVisit[h];
                toVisit.Remove(h);
                visited[h] = pathToMe;
                if (h.getUp() != null && !visited.ContainsKey(h.getUp()) && !toVisit.ContainsKey(h.getUp()))
                    toVisit[h.getUp()] = pathToMe + "1";
                if (h.getUl() != null && !visited.ContainsKey(h.getUl()) && !toVisit.ContainsKey(h.getUl()))
                    toVisit[h.getUl()] = pathToMe + "2";
                if (h.getDl() != null && !visited.ContainsKey(h.getDl()) && !toVisit.ContainsKey(h.getDl()))
                    toVisit[h.getDl()] = pathToMe + "3";
                if (h.getDn() != null && !visited.ContainsKey(h.getDn()) && !toVisit.ContainsKey(h.getDn()))
                    toVisit[h.getDn()] = pathToMe + "4";
                if (h.getDr() != null && !visited.ContainsKey(h.getDr()) && !toVisit.ContainsKey(h.getDr()))
                    toVisit[h.getDr()] = pathToMe + "5";
                if (h.getUr() != null && !visited.ContainsKey(h.getUr()) && !toVisit.ContainsKey(h.getUr()))
                    toVisit[h.getUr()] = pathToMe + "6";
            }
        }
        if (visited.ContainsKey(end))
            return visited[end];
        else
            return "0";
    }

    private string getHexPath(Hex start, Hex end)
    {
        if (end == null)
            return "0";

        Dictionary<Hex, string> d = new Dictionary<Hex, string>();
        getHexDistanceHelper(start, end, d, "");
        if (d.ContainsKey(end))
            return d[end];
        else
            return "0";
    }

    private void getHexDistanceHelper(Hex start, Hex end, Dictionary<Hex, string> visited, string pth)
    {
        if (!visited.ContainsKey(start))
        {
            visited.Add(start, pth);
        }
        else if (visited[start].Length > pth.Length)
        {
            visited[start] = pth;
        }
        else
            return;
        if (start.getUp() != null)
            getHexDistanceHelper(start.getUp(), end, visited, pth + "1");
        if (start.getUl() != null)
            getHexDistanceHelper(start.getUl(), end, visited, pth + "2");
        if (start.getDl() != null)
            getHexDistanceHelper(start.getDl(), end, visited, pth + "3");
        if (start.getDn() != null)
            getHexDistanceHelper(start.getDn(), end, visited, pth + "4");
        if (start.getDr() != null)
            getHexDistanceHelper(start.getDr(), end, visited, pth + "5");
        if (start.getUr() != null)
            getHexDistanceHelper(start.getUr(), end, visited, pth + "6");
    }

    public int getAIState()
    {
        return aiState;
    }
}
