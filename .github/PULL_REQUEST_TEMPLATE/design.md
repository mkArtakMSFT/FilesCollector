## Summary
Our process for agreeing changes and feature designs is too ad-hoc and could be improved.

## Motivation
We don't write down or keep track of designs for changes/features

Implementations sometimes require many rounds of update as we discover, debate, and change the goals during code reviews
Others cannot easily read summaries of what's happening/happened
We don't collaborate on designs as much as we could

Instead of combining input from multiple team members, often just one person owns a design/delivery process, with others ignoring it besides superficial code review after it's done
We give no guidance to community contributors about how to agree on designs

So, often we receive PRs that we can't accept due to mismatches of goals
Goals
Provide a convenient, ready-made method for developers (including community members) to:

... put their change/feature proposals into a written form before coding
... get high-level design input from a wider range of people
... focus on agreeing requirements before selecting a particular implementation
Non-goals
Mandating this process be applied to all changes
Mandating that some kind of sign-off is achieved on a design document before implementation
Basically, developers should be free to choose at what level of detail to go through this process based on their judgement about the particular work item. Over time we will hopefully establish cultural norms around what is useful in what cases.

Scenarios
New user-facing features
Changes to existing user-facing features
Changes to the repo's internal workflow that substantially affect many developers
Risks
This might turn out to be overly bureaucratic, slowing us down and blocking innovation through excessive debate over designs/proposals
People might find it hard to judge how much design is needed, ranging from a one-sentence statement to the actual final source code.
Community members might feel it's raising the bar to contribution too much
If it doesn't catch on, people might feel jaded about the failure to improve our processes
Interactions with other parts of our processes
Mainly this depends on what implementation is chosen. If we choose to have a high-level RFC-type document preceding an implementation, then an aspect of code review would be thinking through whether the result matches the design, and possibly updating the design retrospectively.
