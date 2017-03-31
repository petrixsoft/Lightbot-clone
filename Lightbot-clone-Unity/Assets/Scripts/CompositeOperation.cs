using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeOperation : BotOperation 
{
	private List<BotOperation> opList;

	public CompositeOperation()
	{
		opList = new List<BotOperation> ();
	}

	public override void ValidateOperation ()
	{
		base.ValidateOperation ();
	}

	public override void RunOperation ()
	{
		base.RunOperation ();
	}
}
